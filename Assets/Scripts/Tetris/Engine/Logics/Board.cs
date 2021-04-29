namespace Selftris.Tetris.Engine.Logics
{
    class Board : Logic
    {
        public Board() {}

        private const int ceilingHeight = 43;
        public int[][] occupancy = new int[ceilingHeight][];
        public int curPieceID;
        public Position curPiecePos;
        public Rotation curPieceRot;

        public override void Update(float dt) {}

        public override void UpdateConfig(LogicConfig config) { }

        public void Clear()     // clear the board to empty
        {

        }
    }
}
