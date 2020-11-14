using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public Board[] playersBoard;
    public GameSettings settings;

    #region Initiation Methods
    public GameObject agent;

    public void GameReset()     // call before every episodes start
    {
        for (int i = 0; i < playersBoard.Length; i++)
        {
            playersBoard[i].BoardReset();
        }
    }

    void InitPlayers()      // call before training
    {
        playerBagInd = new int[settings.playerCount];
        playersBoard = new Board[settings.playerCount];
        for (int i = 0; i < settings.playerCount; i++)
        {
            playerBagInd[i] = -1;
            GameObject newAgent = Instantiate(agent, gameObject.transform);
            Vector3 pos = new Vector3(settings.spacing * (i - (settings.playerCount - 1) / 2f), 0f, 0f);
            Board newBoard = newAgent.GetComponent<InitAgent>().CreateAgent(i, pos);
            playersBoard[i] = newBoard;
        }
    }

    void Start()
    {
        InitPlayers();
        GameReset();
    }
    #endregion

    #region Random Bag
    public List<int> bag = new List<int>();
    public int bagInd = -1;
    public int[] playerBagInd;

    public Piece RequestPiece(int playerInd)
    {
        Piece newPiece;
        int curPlayerBagInd = playerBagInd[playerInd];
        if (curPlayerBagInd > bagInd) { throw new System.IndexOutOfRangeException("Player is requesting too fast!"); }    // Debugging
        if (curPlayerBagInd == bagInd)
        {
            newPiece = SamplePiece();
        }
        else
        {
            newPiece = GetPieceByInd(bag[curPlayerBagInd + 1]);
        }
        playerBagInd[playerInd]++;
        CutHistory();
        return newPiece;
    }

    Piece SamplePiece()     // 7-Bag Sampling
    {
        bagInd++;
        int ind = bagInd % 7;
        int cycle = (bagInd - ind) / 7;
        int randInd = Random.Range(0, 7 - ind);
        List<int> curBag = new List<int>() { 0, 1, 2, 3, 4, 5, 6 };
        for (int i = 0; i < ind; i++)
        {
            curBag.Remove(bag[cycle * 7 + i]);
        }
        int returnInd = curBag[randInd];
        bag.Add(returnInd);
        return GetPieceByInd(returnInd);
    }

    Piece GetPieceByInd(int ind)
    {
        switch (ind)
        {
            case 0:
                return new OPiece();
            case 1:
                return new IPiece();
            case 2:
                return new TPiece();
            case 3:
                return new SPiece();
            case 4:
                return new ZPiece();
            case 5:
                return new LPiece();
            case 6:
                return new JPiece();
            default:
                throw new System.IndexOutOfRangeException("Random Index Out of Bound!");
        }
    }

    void CutHistory()   // cut off bag history to save memory
    {
        for (int i = 0; i < playerBagInd.Length; i++)
        {
            if (playerBagInd[i] < 7)
            {
                return;
            }
        }
        for (int i = 0; i < playerBagInd.Length; i++)
        {
            playerBagInd[i] -= 7;
        }
        bagInd -= 7;
        bag.RemoveRange(0, 7);
    }
    #endregion
    public void SendLines(int cleared, int spinType, int perfectClear)    // spinType: 0 = no tspin, 1 = mini tspin, 2 = proper tspin
    {
        if (perfectClear == 1)
        {
            Debug.Log("Perfect Clear!");
        }
    }

    void Update()
    {
        for (int i = 0; i < playersBoard.Length; i++)
        {
            playersBoard[i].Step(Time.deltaTime);
            playersBoard[i].Render();
        }
    }
}