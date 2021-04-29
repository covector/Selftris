using UnityEngine;
using UnityEngine.UI;
using Selftris.Tetris.Engine;
using Selftris.Tetris.Engine.Logics;
using System.Linq;

namespace Selftris.Tetris.Unity
{
    class QualitativeTest : MonoBehaviour
    {
        Player player;

        public Color emptyColor;
        public Color occupiedColor;
        public Color activeColor;   // color for the current piece
        public RawImage renderBoard;


        public TestScenario GetScenario(string scenario)
        {
            TestScenario testScenario;
            switch (scenario)
            {
                case "render":
                    testScenario = new TestScenario
                    {
                        curPieceID = 0,
                        curPieceRot = new Rotation(0),
                        curPiecePos = new Position(4, 10),
                        occupancy = GetEmptyOccupancy(),
                        include = new string[] { "board" }
                    };
                    testScenario.occupancy[2] = new int[10] { -1, -1, -1, -1, -1, -1, 0, -1, -1, -1 };
                    testScenario.occupancy[1] = new int[10] { -1, -1, -1, -1, 0, 0, -1, -1, -1, -1 };
                    testScenario.occupancy[0] = new int[10] { -1, -1, -1, -1, 0, 0, -1, -1, -1, -1 };
                    return testScenario;
                case "gravity":
                    testScenario = new TestScenario
                    {
                        curPieceID = 0,
                        curPieceRot = new Rotation(0),
                        curPiecePos = new Position(4, 20),
                        occupancy = GetEmptyOccupancy(),
                        include = new string[] { "board", "utils", "cs", "gravity" }
                    };
                    testScenario.occupancy[2] = new int[10] { -1, -1, -1, -1, 0, -1, -1, -1, -1, -1 };
                    testScenario.occupancy[1] = new int[10] { -1, -1, -1, -1, 0, 0, -1, -1, -1, -1 };
                    testScenario.occupancy[0] = new int[10] { -1, -1, -1, -1, 0, 0, -1, -1, -1, -1 };
                    return testScenario;
            }
            return new TestScenario();
        }

        private void Start()
        {
            player = new Player(0, null);
            PiecesManager.InitInfo();

            // set up the scenario
            TestScenario scenario = GetScenario("gravity");
            Board board = (Board) player.GetLogic("board");
            board.curPieceID = scenario.curPieceID;
            board.curPieceRot = scenario.curPieceRot;
            board.curPiecePos = scenario.curPiecePos;
            board.occupancy = scenario.occupancy;

            // remove all logics that are not in the include array
            string[] allLogics = player.GetAllLogicKey();
            for (int i = 0; i < allLogics.Length; i++)
            {
                if (!scenario.include.Contains(allLogics[i]))
                {
                    player.RemoveLogic(allLogics[i]);
                }
            }

            // add the board renderer logic
            player.AddLogic("renderer", new BoardRenderer(emptyColor, occupiedColor, activeColor, renderBoard));

            //foreach (string key in player.GetAllLogicKey()) { Debug.Log(key); }
        }

        private int[][] GetEmptyOccupancy()
        {
            int[][] occupancy = new int[43][];
            for (int i = 0; i < occupancy.Length; i++)
            {
                occupancy[i] = new int[10] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 };
            }
            return occupancy;
        }

        private void Update()
        {
            player.Update(Time.deltaTime);
        }
    }

    struct TestScenario
    {
        public int curPieceID;
        public Rotation curPieceRot;
        public Position curPiecePos;
        public int[][] occupancy;
        public string[] include;
    }
}
