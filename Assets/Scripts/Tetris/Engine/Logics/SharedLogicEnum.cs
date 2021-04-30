namespace Selftris.Tetris.Engine.Logics
{
    /// <summary>
    /// Binary identifier for shared predefined logics.
    /// </summary>
    [System.Flags]
    public enum SharedLogicEnum
    {
        /// <summary>Identifier for <see cref="SharedPredefined.PiecesManager"/></summary>
        PIECES = 1 << 0,

        /// <summary>Identifier for all shared predefined logics.</summary>
        ALL = PIECES
    }
}
