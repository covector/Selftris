using UnityEngine;

[System.Serializable]
public class Tester
{
    private bool[][] occupancy;
    private CurPiece curPiece;

    public Tester(bool[][] occupancy, CurPiece curPiece)
    {
        this.occupancy = occupancy;
        this.curPiece = curPiece;
    }

    public bool OccupationTest(Vector2Int pos, int rot)    // checking whether a translation is allowed
    {
        Vector2Int[] checkBlock = curPiece.OccupationTest(rot);    // the block offset from the origin (the origin is not the pivot for I and O piece)

        for (int i = 0; i < checkBlock.Length; i++)
        {
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
}

