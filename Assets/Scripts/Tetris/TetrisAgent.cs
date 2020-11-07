using UnityEngine;

public class TetrisAgent : MonoBehaviour
{
    public Game gameMaster;
    public int playerInd;
    void Update()
    {
        int horizontal = (int)Input.GetAxisRaw("Horizontal");
        int vertical = (int)Input.GetAxisRaw("Vertical");
        gameMaster.Horizontal(playerInd, horizontal);
        gameMaster.SoftDrop(playerInd, vertical == -1);

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            gameMaster.Rotate(playerInd, -1);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            gameMaster.Rotate(playerInd, 1);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            gameMaster.HardDrop(playerInd);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            gameMaster.Hold(playerInd);
        }
        
    }
}
