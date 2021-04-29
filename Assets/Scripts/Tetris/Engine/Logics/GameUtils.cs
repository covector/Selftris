namespace Selftris.Tetris.Engine.Logics
{
    class GameUtils : Logic
    {
        public GameUtils() { }

        public override void Update(float dt) { }

        public override void UpdateConfig(LogicConfig config) { }

        public bool CheckOccupation(int ID, Position pos, int rot)
        {
            Board board = (Board)GetLogic("board");
            Position[] positions = PiecesManager.GetOccupation(ID, rot);
            for (int i = 0; i < positions.Length; i++)
            {
                Position absPos = positions[i] + pos;
                if (board.occupancy[absPos.y][absPos.x] >= 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
