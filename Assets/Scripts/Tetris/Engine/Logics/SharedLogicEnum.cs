namespace Selftris.Tetris.Engine.Logics
{
    [System.Flags]
    public enum SharedLogicEnum
    {
        PIECES = 1 << 0,
        ALL = PIECES
    }
}
