using UnityEngine;

public class Game : MonoBehaviour
{
    public Board[] playersBoard;
    public GameSettings settings;

    public void GameReset()
    {
        for (int i = 0; i < playersBoard.Length; i++)
        {
            playersBoard[i].BoardReset();
        }
    }

    public Piece RequestPiece(int playerInd)
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

    void Update()
    {
        for (int i = 0; i < playersBoard.Length; i++)
        {
            playersBoard[i].Step(Time.deltaTime);
            playersBoard[i].Render();
        }
    }
    void Start()
    {
        GameReset();
    }
}