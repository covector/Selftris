using UnityEngine;
using UnityEngine.UI;
using Selftris.Tetris.Engine;
using Selftris.Tetris.Engine.Logics;

namespace Selftris.Tetris.Unity.Test
{
    class RenderTest : MonoBehaviour
    {
        Player player;

        public Color emptyColor;
        public Color occupiedColor;
        public Color activeColor;   // color for the current piece
        public RawImage renderBoard;

        private void Start()
        {
            player = new Player(0);
            player.AddLogic("renderer", new BoardRenderer(emptyColor, occupiedColor, activeColor, renderBoard));
            PiecesManager.InitInfo();

            Board board = (Board) player.GetLogic("board");
            board.curPieceID = 0;
            board.curPieceRot = 0;
            board.curPiecePos = new Vector2Int(4, 25);
            board.occupancy = new int[43][]
            {
                new int[10] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 },
                new int[10] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 },
                new int[10] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 },
                new int[10] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 },
                new int[10] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 },
                new int[10] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 },
                new int[10] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 },
                new int[10] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 },
                new int[10] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 },
                new int[10] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 },
                new int[10] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 },
                new int[10] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 },
                new int[10] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 },
                new int[10] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 },
                new int[10] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 },
                new int[10] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 },
                new int[10] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 },
                new int[10] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 },
                new int[10] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 },
                new int[10] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 },
                new int[10] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 },
                new int[10] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 },
                new int[10] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 },
                new int[10] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 },
                new int[10] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 },
                new int[10] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 },
                new int[10] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 },
                new int[10] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 },
                new int[10] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 },
                new int[10] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 },
                new int[10] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 },
                new int[10] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 },
                new int[10] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 },
                new int[10] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 },
                new int[10] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 },
                new int[10] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 },
                new int[10] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 },
                new int[10] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 },
                new int[10] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 },
                new int[10] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 },
                new int[10] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 },
                new int[10] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 },
                new int[10] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 }
            };
        }

        private void Update()
        {
            player.Update(Time.deltaTime);
        }
    }
}
