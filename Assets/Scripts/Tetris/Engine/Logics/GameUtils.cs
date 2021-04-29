using UnityEngine;

namespace Selftris.Tetris.Engine.Logics
{
    class GameUtils : Logic
    {
        public GameUtils() { }

        public override void Update(float dt) { }

        public bool CheckOccupation(int ID, Vector2Int pos, int rot)
        {
            Board board = (Board)GetLogic("board");
            Vector2Int[] positions = PiecesManager.GetOccupation(ID, rot);
            for (int i = 0; i < positions.Length; i++)
            {
                Vector2Int absPos = positions[i] + pos;
                if (board.occupancy[absPos.y][absPos.x] >= 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
