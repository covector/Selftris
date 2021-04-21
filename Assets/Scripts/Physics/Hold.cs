using UnityEngine;

[System.Serializable]
public class Hold
{
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
}

