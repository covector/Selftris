using UnityEngine;

namespace Selftris.Tetris.Engine.Logics
{
    public class Board : Logic
    {
        public Board() {}

        private Player player;

        private const int ceilingHeight = 43;
        int[][] occupancy = new int[ceilingHeight][];
        int curPieceID;
        Vector2Int curPiecePos;
        int curPieceRot;

        public override void Update(float dt) {}

        public void Clear()
        {

        }
    }
}
