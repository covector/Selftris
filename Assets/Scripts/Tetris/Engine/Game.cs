using Selftris.Tetris.Engine.Logics;
using System.Collections.Generic;

namespace Selftris.Tetris.Engine
{
    public class Game
    {
        public Game(GameConfig gameConfig)
        {
            sharedBefore = new Dictionary<string, SharedLogic>();
            sharedBeforePriority = new string[] { };
            sharedAfter = new Dictionary<string, SharedLogic>();
            sharedAfterPriority = new string[] { };
            sharedConfig = gameConfig.sharedConfig;

            players = new Player[gameConfig.playerCount];
            for (int i = 0; i < gameConfig.playerCount; i++)
            {
                players[i] = new Player(i, this, gameConfig.logicConfig);
            }
        }

        public Player[] players { get; }
        public SharedLogicConfig sharedConfig;
        private Dictionary<string, SharedLogic> sharedBefore;
        private string[] sharedBeforePriority;
        private Dictionary<string, SharedLogic> sharedAfter;
        private string[] sharedAfterPriority;

        public GameState Update(float dt)
        {
            bool[] playerAlive = new bool[players.Length];
            int aliveCount = 0;

            // Execute shared logics before player

            // Execute personal logics
            for (int i = 0; i < players.Length; i++)
            {
                players[i].Update(dt);
                bool alive = players[i].alive;
                if (alive)
                {
                    playerAlive[i] = true;
                    aliveCount++;
                }
            }

            // Execute shared logics after player

            return new GameState(playerAlive, aliveCount <= 1);
        }

        public SharedLogic GetSharedLogic(string key)
        {

        }

        public string[] GetAllSharedLogicKey()
        {

        }

        public void AddSharedLogic(string key, SharedLogic logic, bool beforePlayer = true, int priority = -1)
        {

        }

        public void RemoveSharedLogic(string key)
        {

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
        public GameConfig(int playerCount, LogicConfig logicConfig, SharedLogicConfig sharedConfig)
        {
            this.playerCount = playerCount;
            this.logicConfig = logicConfig;
            this.sharedConfig = sharedConfig;
        }

        public int playerCount { get; }
        public LogicConfig logicConfig { get; }
        public SharedLogicConfig sharedConfig { get; }
    }
}