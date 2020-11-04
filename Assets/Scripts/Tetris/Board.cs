using System;
using UnityEngine;

[Serializable]
public class Board
{
    public Piece curPiece;
    public Vector2Int curPos;
    public int curRot;
    /* curRot:
        1
        |
    2-------0
        |
        3
    */
    public bool[,] occupancy = new bool[10, 20];

    public Piece Hold(Piece newPiece)
    {
        Piece oldPiece = curPiece;
        SpawnPiece(newPiece);
        return oldPiece;
    }

    public void SpawnPiece(Piece newPiece)
    {
        curPiece = newPiece;
        curPos = newPiece.initPos();
    }

    public void Horizontal(int direction)
    {

    }

    public void Vertical(int direction)
    {

    }

    public int CheckClear()
    {

    }

    public void Rotate(int direction)
    {
        int oldRot = curRot;
        curRot = (curRot + direction + 4) % 4;  // Assumed that |direction| <= 4
        if (!WallKickTest(oldRot, direction))
        {
            curRot = oldRot;
        }
    }

    public bool WallKickTest(int oldRot, int direction)
    {
        Vector2Int[,] checkBlock = curPiece.WallKickTest(oldRot, direction);   // [Test Index, Individual block in a piece]
        
        for (int i = 0; i < checkBlock.GetLength(0); i++)
        {
            for (int j = 0; j < checkBlock.GetLength(1); j++)
            {
                checkBlock[i, j] += curPos;
                Vector2Int pos = checkBlock[i, j];

                bool outOfBoundTest = pos.x > 9 || pos.x < 0 || pos.y > 19 || pos.y < 0;
                bool occupiedTest = occupancy[pos.x, pos.y];

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
}
