/*
    idk how to use inheritance and stuff, so the code is quite ugly, probably gonna clean it up some time later
*/
using UnityEngine;

[System.Serializable]
public abstract class Piece
{
    public abstract int pieceInd { get; }

    public abstract Vector2Int InitPos();

    public abstract Vector2Int[] WallKickTest(int oldRot, int direction, int curRot);

    public abstract Vector2Int[] OccupationTest(int curRot);
}

[System.Serializable]
public class OPiece : Piece
{
    public override int pieceInd { get { return 0; } }
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
    public override int pieceInd { get { return 1; } }
    private static Vector2Int[][] occupationTable = new Vector2Int[4][] {
        new Vector2Int[4] {  new Vector2Int(-1, 0), new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(2, 0) },
        new Vector2Int[4] {  new Vector2Int(0, -2), new Vector2Int(0, -1), new Vector2Int(0, 0), new Vector2Int(0, 1) },
        new Vector2Int[4] {  new Vector2Int(2, -1), new Vector2Int(1, -1), new Vector2Int(0, -1), new Vector2Int(-1, -1) },
        new Vector2Int[4] {  new Vector2Int(1, -1), new Vector2Int(1, 0), new Vector2Int(1, 1), new Vector2Int(1, 2) }
    };
    private static Vector2Int[][] wallKickTable = new Vector2Int[8][]
    {
        new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(-2, 0), new Vector2Int(1, 0), new Vector2Int(-2, -1) , new Vector2Int(1, 2) },
        new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(-2, 0), new Vector2Int(1, -2) , new Vector2Int(-2, 1) },
        new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(2, 0), new Vector2Int(-1, 0), new Vector2Int(2, 1) , new Vector2Int(-1, -2) },
        new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(2, 0), new Vector2Int(-1, 2) , new Vector2Int(2, -1) },
        new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(2, 0), new Vector2Int(-1, 2) , new Vector2Int(2, -1) },
        new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(-2, 0), new Vector2Int(1, 0), new Vector2Int(-2, -1) , new Vector2Int(1, 2) },
        new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(-2, 0), new Vector2Int(1, -2) , new Vector2Int(-2, 1) },
        new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(2, 0), new Vector2Int(-1, 0), new Vector2Int(2, 1) , new Vector2Int(-1, -2) }
    };
    public IPiece() { }
    public override Vector2Int InitPos()
    {
        return new Vector2Int(4, 19);
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

[System.Serializable]
public class TPiece : Piece
{
    public override int pieceInd { get { return 2; } }
    private static Vector2Int[][] occupationTable = new Vector2Int[4][] {
        new Vector2Int[4] {  new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(0, 1), new Vector2Int(-1, 0) },
        new Vector2Int[4] {  new Vector2Int(0, 0), new Vector2Int(0, 1), new Vector2Int(-1, 0), new Vector2Int(0, -1) },
        new Vector2Int[4] {  new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(0, -1), new Vector2Int(1, 0) },
        new Vector2Int[4] {  new Vector2Int(0, 0), new Vector2Int(0, -1), new Vector2Int(1, 0), new Vector2Int(0, 1) }
    };
    private static Vector2Int[][] wallKickTable = new Vector2Int[8][]
    {
        new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(-1, 1), new Vector2Int(0, -2) , new Vector2Int(-1, -2) },
        new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(-1, -1), new Vector2Int(0, 2) , new Vector2Int(-1, 2) },
        new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(1, 1), new Vector2Int(0, -2) , new Vector2Int(-1, 2) },
        new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(1, -1), new Vector2Int(0, 2) , new Vector2Int(1, 2) },
        new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(1, 1), new Vector2Int(0, -2) , new Vector2Int(1, -2) },
        new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(-1, -1), new Vector2Int(0, 2) , new Vector2Int(-1, 2) },
        new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(-1, 1), new Vector2Int(0, -2) , new Vector2Int(-1, -2) },
        new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(1, -1), new Vector2Int(0, 2) , new Vector2Int(1, 2) }
    };
    public TPiece() { }
    public override Vector2Int InitPos()
    {
        return new Vector2Int(4, 18);
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


[System.Serializable]
public class SPiece : Piece
{
    public override int pieceInd { get { return 3; } }
    private static Vector2Int[][] occupationTable = new Vector2Int[4][] {
        new Vector2Int[4] {  new Vector2Int(0, 0), new Vector2Int(1, 1), new Vector2Int(0, 1), new Vector2Int(-1, 0) },
        new Vector2Int[4] {  new Vector2Int(0, 0), new Vector2Int(-1, 1), new Vector2Int(-1, 0), new Vector2Int(0, -1) },
        new Vector2Int[4] {  new Vector2Int(0, 0), new Vector2Int(-1, -1), new Vector2Int(0, -1), new Vector2Int(1, 0) },
        new Vector2Int[4] {  new Vector2Int(0, 0), new Vector2Int(1, -1), new Vector2Int(1, 0), new Vector2Int(0, 1) }
    };
    private static Vector2Int[][] wallKickTable = new Vector2Int[8][]
    {
        new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(-1, 1), new Vector2Int(0, -2) , new Vector2Int(-1, -2) },
        new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(-1, -1), new Vector2Int(0, 2) , new Vector2Int(-1, 2) },
        new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(1, 1), new Vector2Int(0, -2) , new Vector2Int(-1, 2) },
        new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(1, -1), new Vector2Int(0, 2) , new Vector2Int(1, 2) },
        new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(1, 1), new Vector2Int(0, -2) , new Vector2Int(1, -2) },
        new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(-1, -1), new Vector2Int(0, 2) , new Vector2Int(-1, 2) },
        new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(-1, 1), new Vector2Int(0, -2) , new Vector2Int(-1, -2) },
        new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(1, -1), new Vector2Int(0, 2) , new Vector2Int(1, 2) }
    };
    public SPiece() { }
    public override Vector2Int InitPos()
    {
        return new Vector2Int(4, 18);
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


[System.Serializable]
public class ZPiece : Piece
{
    public override int pieceInd { get { return 4; } }
    private static Vector2Int[][] occupationTable = new Vector2Int[4][] {
        new Vector2Int[4] {  new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(0, 1), new Vector2Int(-1, 1) },
        new Vector2Int[4] {  new Vector2Int(0, 0), new Vector2Int(0, 1), new Vector2Int(-1, 0), new Vector2Int(-1, -1) },
        new Vector2Int[4] {  new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(0, -1), new Vector2Int(1, -1) },
        new Vector2Int[4] {  new Vector2Int(0, 0), new Vector2Int(0, -1), new Vector2Int(1, 0), new Vector2Int(1, 1) }
    };
    private static Vector2Int[][] wallKickTable = new Vector2Int[8][]
    {
        new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(-1, 1), new Vector2Int(0, -2) , new Vector2Int(-1, -2) },
        new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(-1, -1), new Vector2Int(0, 2) , new Vector2Int(-1, 2) },
        new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(1, 1), new Vector2Int(0, -2) , new Vector2Int(-1, 2) },
        new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(1, -1), new Vector2Int(0, 2) , new Vector2Int(1, 2) },
        new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(1, 1), new Vector2Int(0, -2) , new Vector2Int(1, -2) },
        new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(-1, -1), new Vector2Int(0, 2) , new Vector2Int(-1, 2) },
        new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(-1, 1), new Vector2Int(0, -2) , new Vector2Int(-1, -2) },
        new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(1, -1), new Vector2Int(0, 2) , new Vector2Int(1, 2) }
    };
    public ZPiece() { }
    public override Vector2Int InitPos()
    {
        return new Vector2Int(4, 18);
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


[System.Serializable]
public class LPiece : Piece
{
    public override int pieceInd { get { return 5; } }
    private static Vector2Int[][] occupationTable = new Vector2Int[4][] {
        new Vector2Int[4] {  new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(1, 1), new Vector2Int(-1, 0) },
        new Vector2Int[4] {  new Vector2Int(0, 0), new Vector2Int(0, 1), new Vector2Int(-1, 1), new Vector2Int(0, -1) },
        new Vector2Int[4] {  new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(-1, -1), new Vector2Int(1, 0) },
        new Vector2Int[4] {  new Vector2Int(0, 0), new Vector2Int(0, -1), new Vector2Int(1, -1), new Vector2Int(0, 1) }
    };
    private static Vector2Int[][] wallKickTable = new Vector2Int[8][]
    {
        new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(-1, 1), new Vector2Int(0, -2) , new Vector2Int(-1, -2) },
        new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(-1, -1), new Vector2Int(0, 2) , new Vector2Int(-1, 2) },
        new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(1, 1), new Vector2Int(0, -2) , new Vector2Int(-1, 2) },
        new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(1, -1), new Vector2Int(0, 2) , new Vector2Int(1, 2) },
        new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(1, 1), new Vector2Int(0, -2) , new Vector2Int(1, -2) },
        new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(-1, -1), new Vector2Int(0, 2) , new Vector2Int(-1, 2) },
        new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(-1, 1), new Vector2Int(0, -2) , new Vector2Int(-1, -2) },
        new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(1, -1), new Vector2Int(0, 2) , new Vector2Int(1, 2) }
    };
    public LPiece() { }
    public override Vector2Int InitPos()
    {
        return new Vector2Int(4, 18);
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

[System.Serializable]
public class JPiece : Piece
{
    public override int pieceInd { get { return 6; } }
    private static Vector2Int[][] occupationTable = new Vector2Int[4][] {
        new Vector2Int[4] {  new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(-1, 1), new Vector2Int(-1, 0) },
        new Vector2Int[4] {  new Vector2Int(0, 0), new Vector2Int(0, 1), new Vector2Int(-1, -1), new Vector2Int(0, -1) },
        new Vector2Int[4] {  new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(1, -1), new Vector2Int(1, 0) },
        new Vector2Int[4] {  new Vector2Int(0, 0), new Vector2Int(0, -1), new Vector2Int(1, 1), new Vector2Int(0, 1) }
    };
    private static Vector2Int[][] wallKickTable = new Vector2Int[8][]
    {
        new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(-1, 1), new Vector2Int(0, -2) , new Vector2Int(-1, -2) },
        new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(-1, -1), new Vector2Int(0, 2) , new Vector2Int(-1, 2) },
        new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(1, 1), new Vector2Int(0, -2) , new Vector2Int(-1, 2) },
        new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(1, -1), new Vector2Int(0, 2) , new Vector2Int(1, 2) },
        new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(1, 1), new Vector2Int(0, -2) , new Vector2Int(1, -2) },
        new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(-1, -1), new Vector2Int(0, 2) , new Vector2Int(-1, 2) },
        new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(-1, 1), new Vector2Int(0, -2) , new Vector2Int(-1, -2) },
        new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(1, -1), new Vector2Int(0, 2) , new Vector2Int(1, 2) }
    };
    public JPiece() { }
    public override Vector2Int InitPos()
    {
        return new Vector2Int(4, 18);
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