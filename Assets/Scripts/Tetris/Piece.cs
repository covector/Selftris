using UnityEngine;

[System.Serializable]
public abstract class Piece
{
    public abstract Vector2Int InitPos();

    public abstract Vector2Int[] WallKickTest(int oldRot, int direction, int curRot);

    public abstract Vector2Int[] OccupationTest(int curRot);
}

[System.Serializable]
public class OPiece : Piece
{
    private static Vector2Int[] occupationTable = new Vector2Int[] { new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(0, 1), new Vector2Int(1, 1) };
    public OPiece() { }
    public override Vector2Int InitPos()
    {
        return new Vector2Int(4, 18);
    }

    public override Vector2Int[] WallKickTest(int oldRot, int direction, int curRot)
    {
        return new Vector2Int[] { };
    }

    public override Vector2Int[] OccupationTest(int curRot)
    {
        return occupationTable;
    }
}

[System.Serializable]
public class IPiece : Piece
{
    private static Vector2Int[][] occupationTable = new Vector2Int[4][] {
        new Vector2Int[4] {  new Vector2Int(-1, 0), new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(2, 0) },
        new Vector2Int[4] {  new Vector2Int(0, 2), new Vector2Int(0, 1), new Vector2Int(0, 0), new Vector2Int(0, -1) },
        new Vector2Int[4] {  new Vector2Int(2, 1), new Vector2Int(1, 1), new Vector2Int(0, 1), new Vector2Int(-1, 1) },
        new Vector2Int[4] {  new Vector2Int(1, -1), new Vector2Int(1, 0), new Vector2Int(1, 1), new Vector2Int(1, 2) }
    };
    private static Vector2Int[][] wallKickTable = new Vector2Int[8][]
    {
        new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(-2, 0), new Vector2Int(1, 0), new Vector2Int(-2, 1) , new Vector2Int(1, -2) },
        new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(-2, 0), new Vector2Int(1, 2) , new Vector2Int(-2, -1) },
        new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(2, 0), new Vector2Int(-1, 0), new Vector2Int(2, -1) , new Vector2Int(-1, 2) },
        new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(2, 0), new Vector2Int(-1, -2) , new Vector2Int(2, 1) },
        new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(2, 0), new Vector2Int(-1, -2) , new Vector2Int(2, 1) },
        new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(-2, 0), new Vector2Int(1, 0), new Vector2Int(-2, 1) , new Vector2Int(1, -2) },
        new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(-2, 0), new Vector2Int(1, 2) , new Vector2Int(-2, -1) },
        new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(2, 0), new Vector2Int(-1, 0), new Vector2Int(2, -1) , new Vector2Int(-1, 2) }
    };
    public IPiece() { }
    public override Vector2Int InitPos()
    {
        return new Vector2Int(4, 0);
    }

    public override Vector2Int[] WallKickTest(int oldRot, int direction, int curRot)
    {
        int index = (direction * 2 + 2) + oldRot;
        return wallKickTable[index];
    }

    public override Vector2Int[] OccupationTest(int curRot)
    {
        return occupationTable[curRot];
    }
}