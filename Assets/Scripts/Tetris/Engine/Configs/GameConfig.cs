using Selftris.Tetris.Engine.Logics;

namespace Selftris.Tetris.Engine.Configs
{
    public readonly struct GameConfig
    {
        public GameConfig(int playerCount, LogicConfig logicConfig, SharedLogicConfig sharedConfig, uint logicSelector = (uint)LogicEnum.ALL, uint sharedLogicSelector = (uint)SharedLogicEnum.ALL)
        {
            this.playerCount = playerCount;
            this.logicSelector = logicSelector;
            this.sharedLogicSelector = sharedLogicSelector;
            this.logicConfig = logicConfig;
            this.sharedConfig = sharedConfig;
        }

        public int playerCount { get; }
        public uint logicSelector { get; }
        public uint sharedLogicSelector { get; }
        public LogicConfig logicConfig { get; }
        public SharedLogicConfig sharedConfig { get; }
    }
}
