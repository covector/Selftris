using Selftris.Tetris.Engine.Logics;
using System.Collections.Generic;
using System.Linq;

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
            for (int i = 0; i < sharedBeforePriority.Length; i++)
            {
                sharedBefore[sharedBeforePriority[i]].Update(dt);
            }

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
            for (int i = 0; i < sharedAfterPriority.Length; i++)
            {
                sharedAfter[sharedAfterPriority[i]].Update(dt);
            }

            return new GameState(playerAlive, aliveCount <= 1);
        }

        public SharedLogic GetSharedLogic(string key)
        {
            if (sharedBeforePriority.Contains(key))
            {
                return sharedBefore[key];
            }
            return sharedAfter[key];
        }

        public string[] GetAllSharedLogicKey()
        {
            return (string[])sharedBeforePriority.Concat(sharedAfterPriority);
        }

        public void AddSharedLogic(string key, SharedLogic logic, bool beforePlayer = true, int priority = -1)
        {
            logic.InjectParent(this);
            logic.UpdateConfig(sharedConfig);
            if (beforePlayer)
            {
                sharedBefore.Add(key, logic);
                sharedBeforePriority = Utils.InsertAt(sharedBeforePriority, priority, key);
            }
            else
            {
                sharedAfter.Add(key, logic);
                sharedAfterPriority = Utils.InsertAt(sharedAfterPriority, priority, key);
            }
        }

        public void RemoveSharedLogic(string key)
        {
            if (sharedBeforePriority.Contains(key))
            {
                sharedBefore.Remove(key);
                sharedBeforePriority = Utils.RemoveFrom(sharedBeforePriority, key);
            }
            else
            {
                sharedAfter.Remove(key);
                sharedAfterPriority = Utils.RemoveFrom(sharedAfterPriority, key);
            }
        }
    }

    [System.Flags]
    public enum PredefSharedLogic
    {
        BOARD = 1 << 0,
        CS = 1 << 1,
        UTILS = 1 << 2,
        GRAVITY = 1 << 3,
        ALL = BOARD | CS | UTILS | GRAVITY
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