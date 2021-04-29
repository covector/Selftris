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
                        curPieceRot = 0,
                        curPiecePos = new Vector2Int(4, 10),
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
                        curPieceRot = 0,
                        curPiecePos = new Vector2Int(4, 20),
                        occupancy = GetEmptyOccupancy(),
                        include = new string[] { "board", "util", "cs", "gravity" }
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
            player = new Player(0);
            PiecesManager.InitInfo();

            TestScenario scenario = GetScenario("gravity");
            Board board = (Board) player.GetLogic("board");
            board.curPieceID = scenario.curPieceID;
            board.curPieceRot = scenario.curPieceRot;
            board.curPiecePos = scenario.curPiecePos;
            board.occupancy = scenario.occupancy;

            string[] allLogics = player.GetAllLogicKey();
            for (int i = 0; i < allLogics.Length; i++)
            {
                if (!scenario.include.Contains(allLogics[i]))
                {
                    player.RemoveLogic(allLogics[i]);
                }
            }

            player.AddLogic("renderer", new BoardRenderer(emptyColor, occupiedColor, activeColor, renderBoard));
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
        public int curPieceRot;
        public Vector2Int curPiecePos;
        public int[][] occupancy;
        public string[] include;
    }
}
