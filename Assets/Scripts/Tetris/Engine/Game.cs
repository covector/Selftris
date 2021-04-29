
namespace Selftris.Tetris.Engine
{
    public class Game
    {
        public Game(GameConfig config)
        {
            players = new Player[config.playerCount];
            for (int i = 0; i < config.playerCount; i++)
            {
                players[i] = new Player(i, this, config.logicConfig);
            }
        }

        public Player[] players { get; }

        public GameState Update(float dt)
        {
            bool[] playerAlive = new bool[players.Length];
            int aliveCount = 0;
            for (int i = 0; i < players.Length; i++)
            {
                bool alive = players[i].Update(dt);
                if (alive)
                {
                    playerAlive[i] = true;
                    aliveCount++;
                }
            }
            return new GameState(playerAlive, aliveCount <= 1);
        }
    }

    public readonly struct GameState
    {
        public GameState(bool[] playerAlive, bool gameEnded)
        {
            this.playerAlive = playerAlive;
            this.gameEnded = gameEnded;
        }

        public bool[] playerAlive { get; }
        public bool gameEnded { get; }
    }

    public readonly struct GameConfig
    {
        public GameConfig(int playerCount, LogicConfig logicConfig)
        {
            this.playerCount = playerCount;
            this.logicConfig = logicConfig;
        }

        public int playerCount { get; }
        public LogicConfig logicConfig { get; }
    }
}