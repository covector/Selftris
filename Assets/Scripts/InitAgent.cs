using UnityEngine;

public class InitAgent : MonoBehaviour
{
    public Canvas display;

    public Board CreateAgent(int agentInd, Vector3 pos)
    {
        display.worldCamera = Camera.main;
        gameObject.transform.localPosition = pos;
        Board gameBoard = GetComponent<Board>();
        TetrisAgent tetrisControl = GetComponent<TetrisAgent>();
        Game gameManager = FindObjectOfType<Game>();
        gameBoard.playerInd = agentInd;
        tetrisControl.playerInd = agentInd;
        gameBoard.gameManager = gameManager;
        tetrisControl.gameManager = gameManager;
        return gameBoard;
    }
}
