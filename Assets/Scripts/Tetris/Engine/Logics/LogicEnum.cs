namespace Selftris.Tetris.Engine.Logics
{
    /// <summary>
    /// Binary identifier for predefined logics.
    /// </summary>
    [System.Flags]
    public enum LogicEnum
    {
        /// <summary>Identifier for <see cref="Board"/></summary>
        BOARD = 1 << 0,

        /// <summary>Identifier for <see cref="ControllerStates"/></summary>
        CS = 1 << 1,

        /// <summary>Identifier for <see cref="GameUtils"/></summary>
        UTILS = 1 << 2,

        /// <summary>Identifier for <see cref="Gravity"/></summary>
        GRAVITY = 1 << 3,

        /// <summary>Identifier for all predefined logics.</summary>
        ALL = BOARD | CS | UTILS | GRAVITY
    }
}
