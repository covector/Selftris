using UnityEngine;

public class TetrisAgent : MonoBehaviour
{
    public Game gameManager;
    public Board gameBoard;
    public int playerInd;
    void Update()
    {
        int horizontal = (int)Input.GetAxisRaw("Horizontal");
        int vertical = (int)Input.GetAxisRaw("Vertical");
        int rotate = 0;
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            rotate = -1;
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            rotate = 1;
        }
        switch (playerInd)
        {
            case 0:
                gameBoard.SetCS(horizontal, vertical == -1, Input.GetKeyDown(KeyCode.Space), rotate, Input.GetKeyDown(KeyCode.X));
                break;
            case 1:
                gameBoard.SetCS(horizontal, vertical == -1, Input.GetKeyDown(KeyCode.G), rotate, Input.GetKeyDown(KeyCode.X));
                break;
        }

    }
}
