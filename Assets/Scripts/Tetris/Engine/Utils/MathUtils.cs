using Selftris.Tetris.Engine.Logics;

namespace Selftris.Tetris.Engine.Utils
{
    /// <summary>
    /// Mathematics utilities class.
    /// </summary>
    public static class MathUtils
    {
        /// <summary>
        /// Compare two binary numbers to see if there is any digit where both numbers is 1.
        /// </summary>
        /// <param name="a">The 1st binary number.</param>
        /// <param name="b">The 2nd binary number.</param>
        /// <returns>Whether there is any digit where both numbers is 1.</returns>
        /// <example>
        /// <code>
        /// // This will return true
        /// MatchBinary(0b_0000_0001, 0b_0000_1111);
        /// // This will return false
        /// MatchBinary(0b_0000_0001, 0b_0000_1110);
        /// </code>
        /// </example>
        public static bool MatchBinary(uint a, uint b)
        {
            return (a & b) > 0;
        }

        /// <summary>
        /// Compare two binary numbers to see if there is any digit where both numbers is 1.
        /// </summary>
        /// <param name="a">The 1st binary number.</param>
        /// <param name="b">The 2nd binary number.</param>
        /// <returns>Whether there is any digit where both numbers is 1.</returns>
        /// <example>
        /// <code>
        /// // LogicEnum.BOARD is 0b_0000_0001
        /// // This will return true
        /// MatchBinary(0b_0000_1101, LogicEnum.BOARD);
        /// // This will return false
        /// MatchBinary(0b_0000_1100, LogicEnum.BOARD);
        /// </code>
        /// </example>
        public static bool MatchBinary(uint a, LogicEnum b)
        {
            return MatchBinary(a, (uint)b);
        }

        /// <summary>
        /// Compare two binary numbers to see if there is any digit where both numbers is 1.
        /// </summary>
        /// <param name="a">The 1st binary number.</param>
        /// <param name="b">The 2nd binary number.</param>
        /// <returns>Whether there is any digit where both numbers is 1.</returns>
        /// <example>
        /// <code>
        /// // SharedLogicEnum.PIECES is 0b_0000_0001
        /// // This will return true
        /// MatchBinary(0b_0000_1101, SharedLogicEnum.PIECES);
        /// // This will return false
        /// MatchBinary(0b_0000_1100, SharedLogicEnum.PIECES);
        /// </code>
        /// </example>
        public static bool MatchBinary(uint a, SharedLogicEnum b)
        {
            return MatchBinary(a, (uint)b);
        }
    }
}
