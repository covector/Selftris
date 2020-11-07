using System;
using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour
{
    public bool[][] occupancy = new bool[20][];
    public Color[] colorOccupancy = new Color[200];
    public Color emptyColor;
    public Color occupiedColor;
    public Color activeColor;
    public RawImage renderBoard;
    public Piece curPiece;
    // TopLeft - (0, 0)
    public Vector2Int curPos;
    /* curRot:
        1
        |
    2-------0
        |
        3
    */
    public int curRot;
    float dropCoolDown = 0f;
    float lockCoolDown = 0f;
    bool droppable = true;

    public float moveCoolDown = 0f;

    public void ClearBoard()
    {
        for (int i = 0; i < occupancy.Length; i++)
        {
            occupancy[i] = new bool[10] { false, false, false, false, false, false, false, false, false, false };
        }
        
    }

    public Piece Hold(Piece newPiece)
    {
        Piece oldPiece = curPiece;
        SpawnPiece(newPiece);
        return oldPiece;
    }

    public void SpawnPiece(Piece newPiece)
    {
        curPiece = newPiece;
        curPos = newPiece.InitPos();
    }

    public void Horizontal(int delta)
    {
        Vector2Int vectorDelta = new Vector2Int(delta, 0);
        if (OccupationTest(vectorDelta, curRot))
        {
            curPos += vectorDelta;
        }
    }

    public bool Vertical(int delta)
    {
        Vector2Int vectorDelta = new Vector2Int(0, delta);
        if (OccupationTest(vectorDelta, curRot))
        {
            curPos += vectorDelta;
            return true;
        }
        return false;
    }

    public int CheckClear(int startInd)    // Only check for the lowest 4 lines
    {
        int cleared = 0;
        for (int i = startInd; i < occupancy.Length; i++)
        {
            if (i < startInd + 4 && occupancy.Equals(new bool[10] { true, true, true, true, true, true, true, true, true, true }))
            {
                cleared++;
            }
            if (cleared > 0)
            {
                if (i + cleared >= occupancy.Length)
                {
                    occupancy[i] = occupancy[i + cleared];
                }
                else
                {
                    occupancy[i] = new bool[10] { false, false, false, false, false, false, false, false, false, false };
                }
            }
        }
        return cleared;
    }

    public bool Rotate(int direction)
    {
        if (WallKickTest(curRot, direction, curRot))
        {
            curRot = (curRot + direction + 4) % 4;  // Assumed that |direction| <= 4
            return true;
        }
        return false;
    }

    public bool WallKickTest(int oldRot, int direction, int curRot)
    {
        Vector2Int[] wallKick = curPiece.WallKickTest(oldRot, direction, curRot);
        Vector2Int[] checkBlock = curPiece.OccupationTest(curRot);

        for (int i = 0; i < wallKick.Length; i++)
        {
            for (int j = 0; j < checkBlock.Length; j++)
            {
                Vector2Int pos = curPos + checkBlock[j] + wallKick[i];
                
                bool outOfBoundTest = pos.x > 9 || pos.x < 0 || pos.y > 19 || pos.y < 0;
                if (outOfBoundTest)
                {
                    break;
                }

                bool occupiedTest = occupancy[pos.y][pos.x];
                if (occupiedTest)
                {
                    break;
                }

                if (j == checkBlock.Length - 1)
                {
                    return true;
                }
            }
        }

        return false;
    }

    public bool OccupationTest(Vector2Int delta, int curRot)
    {
        Vector2Int[] checkBlock = curPiece.OccupationTest(curRot);

        for (int i = 0; i < checkBlock.Length; i++)
        {
            Vector2Int pos = checkBlock[i] + curPos + delta;

            bool outOfBoundTest = pos.x > 9 || pos.x < 0 || pos.y > 19 || pos.y < 0;
            if (outOfBoundTest)
            {
                break;
            }

            bool occupiedTest = occupancy[pos.y][pos.x];
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

    public int[] Flatten()
    {
        int height = occupancy.Length;
        int width = occupancy[0].Length;
        int[] flattened = new int[height * width];
        for (int i = 0; i < height; i++)
        {
            occupancy[i].CopyTo(flattened, width * i);
        }
        return flattened;
    }

    public void LockPiece()
    {

    }

    public void Render()
    {
        for (int i = 0; i < occupancy.Length; i++)
        {
            for (int j = 0; j < occupancy[0].Length; j++)
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
            colorOccupancy[vecBlock.y * 10 + vecBlock.x] = activeColor;
        }
        Texture2D renderTexture = new Texture2D(10, 20);
        renderTexture.SetPixels(colorOccupancy);
        renderTexture.filterMode = FilterMode.Point;
        renderTexture.Apply(false);
        renderBoard.texture = renderTexture;
    }

    public void Step(float deltaTime, GameSettings settings, bool softDropping, int horizontalControl)
    {
        float dropMultiplier = 1f;
        if (softDropping) { dropMultiplier = settings.dropMultiplier; }
        dropCoolDown += deltaTime * settings.dropSpeed * dropMultiplier;
        if (!droppable) 
        {
            dropCoolDown = 0f;
            lockCoolDown += deltaTime * settings.lockSpeed;
        }
        else
        {
            lockCoolDown = 0f;
        }
        if (dropCoolDown > 1f)
        {
            dropCoolDown -= 1f;
            droppable = Vertical(-1);
        }
        if (lockCoolDown > 1f)
        {
            LockPiece() ;
        }

        if (horizontalControl != 0)
        {
            moveCoolDown += deltaTime * settings.horizontalSpeed * horizontalControl;
        }
        else
        {
            moveCoolDown = 0;
        }
        if (moveCoolDown > 1f || moveCoolDown < -1f)
        {
            int direction = (int)Mathf.Sign(moveCoolDown);
            moveCoolDown -= 1f * direction;
            Horizontal(direction);
        }

        Render();
    }
}