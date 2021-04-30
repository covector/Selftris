using Selftris.Tetris.Engine.Logics;

namespace Selftris.Tetris.Engine.Utils
{
    public static class MathUtils
    {
        public static bool MatchBinary(uint a, uint b)
        {
            return (a & b) > 0;
        }

        public static bool MatchBinary(uint a, LogicEnum b)
        {
            return MatchBinary(a, (uint)b);
        }

        public static bool MatchBinary(uint a, SharedLogicEnum b)
        {
            return MatchBinary(a, (uint)b);
        }
    }
}
