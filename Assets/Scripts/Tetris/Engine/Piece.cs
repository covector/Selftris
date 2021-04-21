using UnityEngine;

namespace Selftris.Tetris.Engine
{
    public struct Piece
    {
        public Vector2Int initPos;
        public Vector2Int[][] occupationTable;
        public Vector2Int[][] wallKickTable;
    }
}
