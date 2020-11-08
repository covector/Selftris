using UnityEngine;

public class Game : MonoBehaviour
{
    // Registering Player Inputs
    public Board[] playersBoard;
    public bool[] held;
    public Piece[] holdingPiece;
    public bool[] softDropping;
    public int[] horizontalControl;
    public GameSettings settings;

    public void HardDrop(int playerInd)
    {
        playersBoard[playerInd].HardDrop();
    }

    public void SoftDrop(int playerInd, bool toggle)
    {
        softDropping[playerInd] = toggle;
    }

    public void Horizontal(int playerInd, int direction)
    {
        horizontalControl[playerInd] = direction;
    }

    public void Hold(int playerInd)
    {
        if (!held[playerInd])
        {
            Piece heldPiece = holdingPiece[playerInd];
            if (heldPiece == null) { heldPiece = InitPiece(); }
            Piece returnPiece = playersBoard[playerInd].Hold(heldPiece);
            holdingPiece[playerInd] = returnPiece;
            held[playerInd] = true;
        }
    }

    public void Rotate(int playerInd, int direction)
    {
        playersBoard[playerInd].Rotate(direction);
    }

    public void GameReset()
    {
        for (int i = 0; i < playersBoard.Length; i++)
        {
            playersBoard[i].ClearBoard();
            Piece newPiece = InitPiece();
            playersBoard[i].SpawnPiece(newPiece);
        }
    }

    private Piece InitPiece()
    {
        Piece newPiece;
        int randomInd = Random.Range(0, 2);
        switch (randomInd)
        {
            case 0:
                newPiece = new OPiece();
                break;
            case 1:
                newPiece = new IPiece();
                break;
            default:
                throw new System.IndexOutOfRangeException("Random Index Out of Bound!");
        }
        return newPiece;
    }

    public void SendLines(int receiverInd)
    {

    }

    public Piece RequestPiece(int playerInd)
    {
        held[playerInd] = false;
        return InitPiece();
    }

    void Update()
    {
        for (int i = 0; i < playersBoard.Length; i++)
        {
            playersBoard[i].Step(Time.deltaTime, settings, softDropping[i], horizontalControl[i]);
        }
    }
    void Start()
    {
        holdingPiece = new Piece[1];
        GameReset();
    }
}