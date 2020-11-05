using System;
using UnityEngine;

[Serializable]
public class Board
{
    public int[][] occupancy = new int[20][];
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

    public void ClearBoard()
    {
        for (int i = 0; i < occupancy.Length; i++)
        {
            occupancy[i] = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
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
        if (OccupationTest(curPos + vectorDelta, curRot))
        {
            curPos += vectorDelta;
            return true;
        }
        return false;
    }

    public int CheckClear()    // Only check for the lowest 4 lines
    {
        int cleared = 0;
        for (int i = 9; i >= 0; i--)
        {
            if (i > 5 && occupancy.Equals(new int[10] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }))
            {
                cleared++;
            }
            if (cleared > 0)
            {
                if (i - cleared >= 0)
                {
                    occupancy[i] = occupancy[i - cleared];
                }
                else
                {
                    occupancy[i] = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                }
            }
        }
        return cleared;
    }

    public void Rotate(int direction)
    {
        if (WallKickTest(curRot, direction, curRot))
        {
            curRot = (curRot + direction + 4) % 4;  // Assumed that |direction| <= 4
        }
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
                bool occupiedTest = occupancy[pos.y][pos.x] == 1;

                if (!outOfBoundTest)
                {
                    if (!occupiedTest)
                    {
                        curPos = pos;
                        if (curPos.x > 9 || curPos.x < 0 || curPos.y > 19 || curPos.y < 0) { throw new IndexOutOfRangeException("Delta position provided by wall kick test is out of bound!"); }    // Debug
                        return true;
                    }
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
            bool occupiedTest = occupancy[pos.y][pos.x] == 1;

            if (!outOfBoundTest)
            {
                if (!occupiedTest)
                {
                    return true;
                }
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
}