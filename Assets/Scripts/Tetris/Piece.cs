using UnityEngine;

[System.Serializable]
public abstract class Piece
{
    public abstract Vector2Int WallKickTest(int oldRot, int direction);
}

[System.Serializable]
public class OPiece : Piece
{
    public override Vector2Int WallKickTest(int oldRot, int direction)
    {

    }
}