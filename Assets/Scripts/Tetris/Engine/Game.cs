using Selftris.Tetris.Engine.Configs;
using Selftris.Tetris.Engine.Logics;
using Selftris.Tetris.Engine.Logics.SharedPredefined;
using Selftris.Tetris.Engine.Utils;
using System.Collections.Generic;
using System.Linq;

namespace Selftris.Tetris.Engine
{
    /// <summary>
    /// Contains all the shared logics and the players in a Tetris game.
    /// </summary>
    public class Game
    {
        /// <param name="gameConfig">The game configuration.</param>
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

        /// <summary>The players in this game.</summary>
        public Player[] players { get; }

        /// <summary>The array indicating which player is alive.</summary>
        public bool[] playerAlive;

        /// <summary>The total count of alive players.</summary>
        public int aliveCount;

        /// <summary>Configuration for the shared logics.</summary>
        public SharedLogicConfig sharedConfig;

        private Dictionary<string, SharedLogic> sharedBefore;

        private string[] sharedBeforePriority;

        private Dictionary<string, SharedLogic> sharedAfter;

        private string[] sharedAfterPriority;

        /// <summary>
        /// Executes shared logics and players' logics in the order of their priority.
        /// This is called every step in the game loop.
        /// </summary>
        /// <param name="dt">Seconds past since last execution of these logics.</param>
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

        /// <summary>
        /// Get a shared logic by its key.
        /// </summary>
        /// <param name="key">The key of the shared logic to be retrieved.</param>
        /// <returns>The shared logic that matched the key.</returns>
        public SharedLogic GetSharedLogic(string key)
        {
            if (sharedBeforePriority.Contains(key))
            {
                return sharedBefore[key];
            }
            return sharedAfter[key];
        }

        /// <summary>
        /// Get keys of all registered shared logics.
        /// </summary>
        /// <returns>An array of shared logic keys.</returns>
        public string[] GetAllSharedLogicKey()
        {
            return (string[])sharedBeforePriority.Concat(sharedAfterPriority);
        }

        /// <summary>
        /// Register a shared logic.
        /// </summary>
        /// <param name="key">A unique identifier for distinguishing from other type of shared logics.</param>
        /// <param name="logic">The shared logic instance to be registered.</param>
        /// <param name="beforePlayer">Whether to execute this logic before executing players' logic.</param>
        /// <param name="priority">0 means it will be the first to be executed, 1 means executing it second, etc.<br />
        /// -1 means executing it last.</param>
        /// <example>
        /// <code>
        /// // This will add the CoOpLogic to the player
        /// // You can reference this shared logic by using the "coop" key
        /// // And this logic will be the second to be executed after all the players' logics because priority is set to 3 and beforePlayer is set to true.
        /// game.AddSharedLogic("coop", new CoOpLogic(), false, 1);
        /// </code>
        /// </example>
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

        /// <summary>
        /// Unregister a shared logic.
        /// </summary>
        /// <param name="key">The key of the shared logic to be unregistered.</param>
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