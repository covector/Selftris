using UnityEngine;

namespace Selftris.Tetris.Engine.Logics
{
    class Board : Logic
    {
        public Board() {}

        private const int ceilingHeight = 43;
        public int[][] occupancy = new int[ceilingHeight][];
        public int curPieceID;
        public Vector2Int curPiecePos;
        public int curPieceRot;

        public override void Update(float dt) {}

        public void Clear()
        {

        }
    }
}
