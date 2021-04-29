namespace Selftris.Tetris.Engine
{
    public struct Piece
    {
        public Position initPos;
        public Position[][] occupationTable;
        public Position[][] wallKickTable;
    }
}
