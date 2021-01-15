using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour
{
    public int playerInd;    // player index
    bool[][] occupancy = new bool[43][];    // the board: true is occupied while false is empty
    Piece curPiece;    // current piece
    Vector2Int curPos;    // current position
    int curRot;    // current rotation
    public Game gameManager;
    public GameSettings settings;


    #region Rotation
    int rotateCS = 0;    // Rotate Controller State

    void RegRot()     // check whether it should rotate (no need check, just for keeping code format uniform)
    {
        if (rotateCS != 0)
        {
            ExeRot(rotateCS);
        }
    }

    void ExeRot(int direction)   // actually rotating the piece
    {
        int newRot = (curRot + direction + 4) % 4;  // Assumed that |direction| <= 4
        if (WallKickTest(curRot, direction, newRot))
        {
            curRot = newRot;
            rotationLast = true;
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
    bool[][] holdPanel = new bool[20][];

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
        if (holding != null)    // not first time holding
        {
            Piece tempPiece = holding;
            holding = curPiece;
            curPiece = tempPiece;
        }
        else    // first time holding
        {
            holding = curPiece;
            curPiece = GetNext();
        }
        curPos = curPiece.InitPos();
        curRot = 0;
        rotationLast = false;
        offset2 = false;
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

    void UpdateHoldPanel(Piece piece)
    {

    }
    #endregion

    #region Clearing
    int[] CheckClear(int startInd)
    {
        int cleared = 0;    // total line cleared
        int perfect = startInd == 0 ? 1 : 0;    // perfect clear or not. If piece dont touch floor, impossible for perfect clear
        for (int i = startInd; i < occupancy.Length; i++)    // deleting row cleared and pulling all the other row down
        {
            if (occupancy[i].All(x => x))    // full row occupation check
            {
                cleared++;
                continue;   // doesnt count as non perfect clear
            }
            else    // copy a row to another row
            {
                occupancy[i - cleared] = occupancy[i];
            }

            if (!occupancy[i].All(x => !x))    // full row empty check
            {
                perfect = 0;
            }
        }
        for (int i = 0; i < cleared; i++)    // filling the top with all empty box
        {
            occupancy[occupancy.Length - i - 1] = new bool[10] { false, false, false, false, false, false, false, false, false, false };
        }
        return new int[] { cleared, perfect };
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
                    if (wallKick[i].y == -2) { offset2 = true; }    // for detecting proper tspin
                    else { offset2 = false; }
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
    List<Piece> previewBuffer = new List<Piece>();
    bool[][] previewPanel = new bool[20][];

    void SpawnPiece()    // spawning new piece
    {
        curPiece = GetNext();
        curPos = curPiece.InitPos();
        curRot = 0;
        rotationLast = false;
        offset2 = false;
        CheckTopOut();
        ResetHold();
    }

    Piece GetNext()
    {
        if (settings.previewCount == 0)
        {
            return gameManager.RequestPiece(playerInd);
        }
        else
        {
            Piece returnPiece = previewBuffer[0];
            previewBuffer.RemoveAt(0);
            previewBuffer.Add(gameManager.RequestPiece(playerInd));
            return returnPiece;
        }
    }

    void SetUpPreview()
    {
        for (int i = 0; i < settings.previewCount; i++)
        {
            previewBuffer.Add(gameManager.RequestPiece(playerInd));
        }
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

    void UpdatePreviewPanel(Piece piece)
    {

    }
    #endregion

    #region Tspin Detection
    public bool rotationLast = false;
    public bool offset2 = false;
    static Vector2Int[][] frontCheck = new Vector2Int[4][]
    {
        new Vector2Int[2] {  new Vector2Int(1, 1), new Vector2Int(-1, 1) },
        new Vector2Int[2] {  new Vector2Int(-1, 1), new Vector2Int(-1, -1) },
        new Vector2Int[2] {  new Vector2Int(-1, -1), new Vector2Int(1, -1) },
        new Vector2Int[2] {  new Vector2Int(1, -1), new Vector2Int(1, 1) }
    };
    static Vector2Int[][] backCheck = new Vector2Int[4][]
    {
        new Vector2Int[2] {  new Vector2Int(-1, -1), new Vector2Int(1, -1) },
        new Vector2Int[2] {  new Vector2Int(1, -1), new Vector2Int(1, 1) },
        new Vector2Int[2] {  new Vector2Int(1, 1), new Vector2Int(-1, 1) },
        new Vector2Int[2] {  new Vector2Int(-1, 1), new Vector2Int(-1, -1) }
    };

    int TspinCheck()
    {
        Vector2Int[] front = frontCheck[curRot];
        int frontBlock = 0;
        for (int i = 0; i < front.Length; i++)
        {
            Vector2Int pos = curPos + front[i];

            bool outOfBoundTest = pos.x > 9 || pos.x < 0 || pos.y < 0;
            if (outOfBoundTest)
            {
                frontBlock++;
                continue;
            }

            bool occupiedTest = occupancy[pos.y][pos.x];
            if (occupiedTest)
            {
                frontBlock++;
                continue;
            }
        }

        Vector2Int[] back = backCheck[curRot];
        int backBlock = 0;
        for (int i = 0; i < back.Length; i++)
        {
            Vector2Int pos = curPos + back[i];

            bool outOfBoundTest = pos.x > 9 || pos.x < 0 || pos.y < 0;
            if (outOfBoundTest)
            {
                backBlock++;
                continue;
            }

            bool occupiedTest = occupancy[pos.y][pos.x];
            if (occupiedTest)
            {
                backBlock++;
                continue;
            }
        }

        bool minRequirement = rotationLast && curPiece.pieceInd == 2 && frontBlock + backBlock >= 3;

        if (minRequirement && (frontBlock == 2 || offset2))    // proper tspin
        {
            return 2;
        }
        if (minRequirement && frontBlock == 1)     // tspin mini
        {
            return 1;
        }
        return 0;
    }
    #endregion

    #region Locking
    float softLockCoolDown = 0f;
    float hardLockCoolDown = 0f;
    int b2bCount = 0;
    int comboCount = 0;

    void LockPiece()
    {
        int stoppedHeight = 22;
        Vector2Int[] checkBlock = curPiece.OccupationTest(curRot);
        for (int i = 0; i < checkBlock.Length; i++)     // set stoppedheight as the y value of the lowest block
        {
            Vector2Int vecBlock = checkBlock[i] + curPos;

            if (vecBlock.y < stoppedHeight)
            {
                stoppedHeight = vecBlock.y;
            }
            if (vecBlock.y < checkBlock.Length)
            {
                occupancy[vecBlock.y][vecBlock.x] = true;
            }
        }
        if (stoppedHeight > 19)     // none of the blocks of the piece are in the board
        {
            ToppedOut();
        }
        int spin = TspinCheck();
        int[] clearingInfo = CheckClear(stoppedHeight);
        if (clearingInfo[0] > 0)
        {
            int inB2B = (clearingInfo[0] == 4 || spin > 0)? 1 : 0;
            b2bCount *= inB2B;
            gameManager.SendLines(clearingInfo[0], spin, clearingInfo[1], b2bCount, comboCount);
            b2bCount += inB2B;
            comboCount++;
        }
        else
        {
            comboCount = 0;
        }
        rotationLast = false;
        offset2 = false;
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

    #region Garbage
    List<int> delayQueue;
    List<float> delayQueueDelay;
    List<int> garbageQueue;

    public void DelayQueuing(int lines, float delay)
    {
        float wait = delay;
        for (int i = 0; i < delayQueue.Count; i++)
        {
            wait -= delayQueueDelay[i];
        }
        delayQueue.Add(lines);
        delayQueueDelay.Add(wait);
    }

    void Queuing(int lines)
    {
        garbageQueue.Add(lines);
    }

    void EvaluateQueue()
    {
        for (int i = 0; i < garbageQueue.Count; i++)
        {
            SpawningLines(garbageQueue[0]);
        }
        garbageQueue = new List<int>();
    }

    void SpawningLines(int lines)
    {
        int emptyInd = Random.Range(0, 10);
        bool[] garbageLine = new bool[10] { true, true, true, true, true, true, true, true, true, true };
        for (int i = occupancy.Length; i >= lines; i++)
        {
            occupancy[i] = occupancy[i - lines];
        }
        for (int i = 0; i < lines; i++)
        {
            occupancy[i] = garbageLine.ToArray();
        }
    }

    void DelayQueueStep(float deltaTime)
    {
        if (delayQueue.Count == 0) { return; }
        delayQueueDelay[0] -= deltaTime;
        for (int i = 0; i < 300; i++)
        {
            if (delayQueueDelay[0] <= 0)
            {
                Queuing(delayQueue[0]);
                delayQueue.RemoveAt(0);
                delayQueueDelay.RemoveAt(0);
            }
            else { break; }
        }
    }

    int LineCancelling(int lines)   // return line left for sending
    {
        int left = lines;
        int queueLength = garbageQueue.Count;
        for (int i = 0; i < queueLength; i++)
        {
            int temp = left;
            left -= garbageQueue[0];
            garbageQueue[0] -= temp;
            
            if (garbageQueue[0] <= 0)
            {
                garbageQueue.RemoveAt(0);
            }
            if (left <= 0)
            {
                return 0;
            }
        }

        int delayQueueLength = delayQueue.Count;
        for (int i = 0; i < delayQueueLength; i++)
        {
            int temp = left;
            left -= delayQueue[0];
            delayQueue[0] -= temp;

            if (garbageQueue[0] <= 0)
            {
                delayQueue.RemoveAt(0);
                delayQueueDelay.RemoveAt(0);
            }
            if (left <= 0)
            {
                return 0;
            }
        }

        return left;
    }
    #endregion

    #region Public Methods
    Color[] colorOccupancy = new Color[400];     // flattened board
    public Color emptyColor;
    public Color occupiedColor;
    public Color activeColor;   // color for the current piece
    public RawImage renderBoard;

    public void Render()
    {
        // Update colorOccupancy vector
        for (int i = 0; i < 20; i++)      // dont render block out of the board
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
        SetUpPreview();
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