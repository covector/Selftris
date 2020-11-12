using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour
{
    public int playerInd;    // player index
    bool[][] occupancy = new bool[23][];    // the board: true is occupied while false is empty
    Piece curPiece;    // current piece
    Vector2Int curPos;    // current position
    int curRot;    // current rotation
    public Game gameManager;
    public GameSettings settings;

    #region Horizontal
    int horizCS = 0;    // Horizontal Movement Controller State
    int lastMove = 0;   // control in the last step
    float moveCoolDown = 0f;     // if >1 then move right, if <1 then move left

    void RegHoriz(float deltaTime)     // check whether the piece should move horizontally
    {
        // Hold to Move
        moveCoolDown += deltaTime * settings.horizontalSpeed * horizCS;

        if (horizCS != lastMove)      // change of the force direction
        {
            moveCoolDown = 0;
        }

        if (horizCS * moveCoolDown > settings.accelerateDelay)    // check if the piece gain enough momentum to move
        {
            moveCoolDown -= horizCS;
            ExeHoriz(horizCS);
        }

        // Tap to Move
        if (lastMove != horizCS && horizCS != 0)
        {
            ExeHoriz(horizCS);
        }

        // Record Move
        lastMove = horizCS;

        // Reset Locking due to Horizontal Movement
        if (horizCS != 0)
        {
            SoftResetLocking();
        }
    }

    void ExeHoriz(int delta)    // actually moving the piece horizontally
    {
        Vector2Int vectorDelta = new Vector2Int(delta, 0);
        if (OccupationTest(vectorDelta, curRot))
        {
            curPos += vectorDelta;
        }
    }

    void SetHorizCS(int value)
    {
        horizCS = value;
    }
    #endregion

    #region Vertical
    bool softDropCS = false;    // Soft Drop Controller State
    float dropCoolDown = 0f;     // if >1 then drop 1 block

    void RegVert(float deltaTime)     // check whether the piece should drop
    {
        if (DropCheck())
        {
            float dropMultiplier = 1f;
            if (softDropCS) { dropMultiplier = settings.dropMultiplier; }
            dropCoolDown += deltaTime * settings.dropSpeed * dropMultiplier;
            if (dropCoolDown > 1f)
            {
                curPos += new Vector2Int(0, -1);
                dropCoolDown -= 1f;
            }
            HardResetLocking();
        }
        else
        {
            dropCoolDown = 0f;
            LockingStep(deltaTime);
        }
    }

    bool hardDropCS;    // Hard Drop Controller State

    void RegHardDrop()     // check whether it should hard drop
    {
        if (hardDropCS)
        {
            int debugging = 0;
            while (ExeVert(-1))
            {
                // Infinite loop prevention
                debugging++;
                if (debugging > 30) { break; }
            }
            LockPiece();
        }
    }

    bool ExeVert(int delta)    // actually moving the piece horizontally
    {
        Vector2Int vectorDelta = new Vector2Int(0, delta);
        if (OccupationTest(vectorDelta, curRot))
        {
            curPos += vectorDelta;
            return true;
        }
        return false;
    }

    bool DropCheck()    // check if dropping downward by 1 block is allowed
    {
        Vector2Int vectorDelta = new Vector2Int(0, -1);
        return OccupationTest(vectorDelta, curRot);
    }

    void SetSoftDropCS(bool value)
    {
        softDropCS = value;
    }

    void SetHardDropCS(bool value)
    {
        hardDropCS = value;
    }
    #endregion

    #region Rotation
    int rotateCS = 0;    // Rotate Controller State

    void RegRot()     // check whether it should rotate (no need check, just for keeping code format uniform)
    {
        ExeRot(rotateCS);
    }

    void ExeRot(int direction)   // actually rotating the piece
    {
        int newRot = (curRot + direction + 4) % 4;  // Assumed that |direction| <= 4
        if (WallKickTest(curRot, direction, newRot))
        {
            curRot = newRot;
        }
    }

    void SetRotCS(int value)
    {
        rotateCS = value;
    }
    #endregion

    #region Hold
    bool holdCS = false;    // Hold Controller State
    bool canHold = true;   // whether the player can hold or not
    Piece holding;   // the piece that is holding

    void RegHold()    // check whether it should hold
    {
        if (holdCS && canHold)
        {
            ExeHold();
            canHold = false;
        }
    }

    void ExeHold()    // actually holding the piece
    {
        if (holding != null)    // first time holding
        {
            Piece tempPiece = holding;
            holding = curPiece;
            curPiece = tempPiece;
        }
        else    // not first time holding
        {
            holding = curPiece;
            curPiece = gameManager.RequestPiece(playerInd);
        }
        curPos = curPiece.InitPos();
        curRot = 0;
        CheckTopOut();
    }

    void SetHoldCS(bool value)
    {
        holdCS = value;
    }
    void ResetHold()    // allow player to hold again
    {
        canHold = true;
    }
    #endregion

    #region Clearing
    int CheckClear(int startInd)
    {
        int cleared = 0;    // total line cleared
        for (int i = startInd; i < occupancy.Length; i++)    // deleting row cleared and pulling all the other row down
        {
            if (occupancy[i].All(x => x))    // full row occupation check
            {
                cleared++;
            }
            else    // copy a row to another row
            {
                occupancy[i - cleared] = occupancy[i];
            }
        }
        for (int i = 0; i < cleared; i++)    // filling the top with all empty box
        {
            occupancy[occupancy.Length - i - 1] = new bool[10] { false, false, false, false, false, false, false, false, false, false };
        }
        return cleared;
    }
    #endregion

    #region Legal Action Test
    bool WallKickTest(int oldRot, int direction, int curRot)    // checking whether a rotation is allowed
    {
        Vector2Int[] wallKick = curPiece.WallKickTest(oldRot, direction, curRot);    // a translational offset applied to the piece, if the rotation is allowed after this translation, the translation will be kept
        Vector2Int[] checkBlock = curPiece.OccupationTest(curRot);    // the block offset from the origin (the origin is not the pivot for I and O piece)

        for (int i = 0; i < wallKick.Length; i++)
        {
            for (int j = 0; j < checkBlock.Length; j++)
            {
                Vector2Int pos = curPos + checkBlock[j] + wallKick[i];    // theoretical end position

                bool outOfBoundTest = pos.x > 9 || pos.x < 0 || pos.y < 0 || pos.y > 22;    // whether the position is out of the board
                if (outOfBoundTest)
                {
                    break;
                }

                bool occupiedTest = occupancy[pos.y][pos.x];    // whether the position is occupied
                if (occupiedTest)
                {
                    break;
                }

                if (j == checkBlock.Length - 1)
                {
                    curPos += wallKick[i];    // apply the translational offset since the rotation is allowed
                    return true;
                }
            }
        }

        return false;
    }

    bool OccupationTest(Vector2Int delta, int curRot)    // checking whether a translation is allowed
    {
        Vector2Int[] checkBlock = curPiece.OccupationTest(curRot);    // the block offset from the origin (the origin is not the pivot for I and O piece)

        for (int i = 0; i < checkBlock.Length; i++)
        {
            Vector2Int pos = checkBlock[i] + curPos + delta;    // theoretical end position

            bool outOfBoundTest = pos.x > 9 || pos.x < 0 || pos.y < 0 || pos.y > 22;    // whether the position is out of the board
            if (outOfBoundTest)
            {
                break;
            }

            bool occupiedTest = occupancy[pos.y][pos.x];    // whether the position is occupied
            if (occupiedTest)
            {
                break;
            }

            if (i == checkBlock.Length - 1)
            {
                return true;
            }
        }

        return false;
    }
    #endregion

    #region Spawning
    void SpawnPiece()    // spawning new piece
    {
        curPiece = gameManager.RequestPiece(playerInd);
        curPos = curPiece.InitPos();
        curRot = 0;
        CheckTopOut();
        ResetHold();
    }

    void CheckTopOut()
    {
        Vector2Int[] checkBlock = curPiece.OccupationTest(curRot);    // the block offset from the origin (the origin is not the pivot for I and O piece)

        for (int i = 0; i < 2; i++)     // allow 1 block above the board
        {
            Vector2Int offset = new Vector2Int(0, i);
            for (int j = 0; j < checkBlock.Length; j++)
            {
                Vector2Int pos = checkBlock[i] + curPos + offset;    // theoretical end position

                bool occupiedTest = occupancy[pos.y][pos.x];    // whether the position is occupied
                if (!occupiedTest)
                {
                    curPos += offset;
                    return;
                }
            }
        }
        ToppedOut();
    }

    void ToppedOut()
    {
        Debug.Log("Player " + playerInd.ToString() + " topped out!");
    }
    #endregion

    #region Locking
    float softLockCoolDown = 0f;
    float hardLockCoolDown = 0f;

    void LockPiece()
    {
        int stoppedHeight = 19;
        Vector2Int[] checkBlock = curPiece.OccupationTest(curRot);
        for (int i = 0; i < checkBlock.Length; i++)     // set stoppedheight as the y value of the lowest block
        {
            Vector2Int vecBlock = checkBlock[i] + curPos;

            if (vecBlock.y < stoppedHeight)
            {
                stoppedHeight = vecBlock.y;
            }
            if (vecBlock.y < 23)
            {
                occupancy[vecBlock.y][vecBlock.x] = true;
            }
        }
        CheckClear(stoppedHeight);
        SpawnPiece();
    }

    void SoftResetLocking()     // reset by moving horizontally
    {
        softLockCoolDown = 0f;
    }

    void HardResetLocking()     // reset by dropping
    {
        softLockCoolDown = 0f;
        hardLockCoolDown = 0f;
    }

    void LockingStep(float deltaTime)
    {
        softLockCoolDown += deltaTime;
        hardLockCoolDown += deltaTime;
        if (softLockCoolDown > settings.softLockSpeed || hardLockCoolDown > settings.hardLockSpeed)
        {
            LockPiece();
        }
    }
    #endregion

    #region Public Methods
    Color[] colorOccupancy = new Color[200];     // flattened board
    public Color emptyColor;
    public Color occupiedColor;
    public Color activeColor;   // color for the current piece
    public RawImage renderBoard;

    public void Render()
    {
        // Update colorOccupancy vector
        for (int i = 0; i < occupancy.Length - 3; i++)      // dont render block out of the board
        {
            for (int j = 0; j < occupancy[0].Length; j++)      // occupated and empty box coloring
            {
                if (occupancy[i][j])
                {
                    colorOccupancy[i * 10 + j] = occupiedColor;
                }
                else
                {
                    colorOccupancy[i * 10 + j] = emptyColor;
                }
            }
        }
        Vector2Int[] checkBlock = curPiece.OccupationTest(curRot);
        for (int i = 0; i < checkBlock.Length; i++)
        {
            Vector2Int vecBlock = checkBlock[i] + curPos;
            if (vecBlock.y > 19) { continue; }      // dont render block out of the board
            colorOccupancy[vecBlock.y * 10 + vecBlock.x] = activeColor;
        }

        // Render it to the RawImage
        Texture2D renderTexture = new Texture2D(10, 20);
        renderTexture.SetPixels(colorOccupancy);
        renderTexture.filterMode = FilterMode.Point;
        renderTexture.Apply(false);
        renderBoard.texture = renderTexture;
    }

    public void BoardReset()     // reset the whole board
    {
        for (int i = 0; i < occupancy.Length; i++)
        {
            occupancy[i] = new bool[10] { false, false, false, false, false, false, false, false, false, false };
        }
        SpawnPiece();
        ResetCS();
    }

    public void Step(float deltaTime)     // calculate game logic
    {
        // check if these control should be executed
        RegHoriz(deltaTime);
        RegRot();
        RegVert(deltaTime);
        RegHardDrop();
        RegHold();

        // reset hold, hard drop, rotation controller state to default
        SetHoldCS(false);
        SetHardDropCS(false);
        SetRotCS(0);
    }

    public void SetCS(int horiz, bool softDrop, bool hardDrop, int rotate, bool hold)
    {
        SetHorizCS(horiz);
        SetSoftDropCS(softDrop);
        SetHardDropCS(hardDrop);
        SetRotCS(rotate);
        SetHoldCS(hold);
    }

    public void ResetCS()
    {
        SetHorizCS(0);
        SetSoftDropCS(false);
        SetHardDropCS(false);
        SetRotCS(0);
        SetHoldCS(false);
    }
    #endregion
}