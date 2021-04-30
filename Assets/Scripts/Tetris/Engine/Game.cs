using Selftris.Tetris.Engine.Configs;
using Selftris.Tetris.Engine.Logics;
using Selftris.Tetris.Engine.Logics.SharedPredefined;
using Selftris.Tetris.Engine.Utils;
using System.Collections.Generic;
using System.Linq;

namespace Selftris.Tetris.Engine
{
    public class Game
    {
        public Game(GameConfig gameConfig)
        {
            playerAlive = new bool[gameConfig.playerCount];
            sharedBefore = new Dictionary<string, SharedLogic>();
            sharedBeforePriority = new string[] { };
            sharedAfter = new Dictionary<string, SharedLogic>();
            sharedAfterPriority = new string[] { };
            sharedConfig = gameConfig.sharedConfig;

            // Creates players
            players = new Player[gameConfig.playerCount];
            for (int i = 0; i < gameConfig.playerCount; i++)
            {
                players[i] = new Player(i, this, gameConfig.logicConfig, gameConfig.logicSelector);
            }

            // Register shared logics
            if (MathUtils.MatchBinary(gameConfig.sharedLogicSelector, SharedLogicEnum.PIECES)) { AddSharedLogic("pieces", new PiecesManager()); }
        }

        public Player[] players { get; }
        public bool[] playerAlive;
        public int aliveCount;
        public SharedLogicConfig sharedConfig;
        private Dictionary<string, SharedLogic> sharedBefore;
        private string[] sharedBeforePriority;
        private Dictionary<string, SharedLogic> sharedAfter;
        private string[] sharedAfterPriority;

        public void Update(float dt)
        {
            aliveCount = 0;

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
                playerAlive[i] = alive;
                aliveCount += alive ? 1 : 0;
            }

            // Execute shared logics after player
            for (int i = 0; i < sharedAfterPriority.Length; i++)
            {
                sharedAfter[sharedAfterPriority[i]].Update(dt);
            }
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
                sharedBeforePriority = ArrayUtils.InsertAt(sharedBeforePriority, priority, key);
            }
            else
            {
                sharedAfter.Add(key, logic);
                sharedAfterPriority = ArrayUtils.InsertAt(sharedAfterPriority, priority, key);
            }
        }

        public void RemoveSharedLogic(string key)
        {
            if (sharedBeforePriority.Contains(key))
            {
                sharedBefore.Remove(key);
                sharedBeforePriority = ArrayUtils.RemoveFrom(sharedBeforePriority, key);
            }
            else
            {
                sharedAfter.Remove(key);
                sharedAfterPriority = ArrayUtils.RemoveFrom(sharedAfterPriority, key);
            }
        }
    }
}