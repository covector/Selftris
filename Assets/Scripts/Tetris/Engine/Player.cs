using Selftris.Tetris.Engine.Configs;
using Selftris.Tetris.Engine.Logics;
using Selftris.Tetris.Engine.Logics.Predefined;
using Selftris.Tetris.Engine.Utils;
using System.Collections.Generic;

namespace Selftris.Tetris.Engine {
    /// <summary>
    /// Contains all the logics and states of one player in a Tetris game.<br />
    /// Usually attached to a <see cref="Game" /> instance.
    /// </summary>
    public class Player
    {
        /// <param name="index">The player index for uniquely identifying a player in a game.</param>
        /// <param name="game">Reference of the Game instance that owns this player object.</param>
        /// <param name="logicConfig">Configuration for the logics.</param>
        /// <param name="selectedLogic">Select the predefined logics being used.</param>
        public Player(int index, Game game, LogicConfig logicConfig, uint selectedLogic = (uint)LogicEnum.ALL)
        {
            // Instantiate variables
            this.index = index;
            this.game = game;
            this.logicConfig = logicConfig;
            logics = new Dictionary<string, Logic>();
            logicPriority = new string[] {};

            // Register logics
            if ((selectedLogic & (uint)LogicEnum.BOARD) > 0) { AddLogic("board", new Board()); }
            if ((selectedLogic & (uint)LogicEnum.CS) > 0) { AddLogic("cs", new ControllerStates()); }
            if ((selectedLogic & (uint)LogicEnum.UTILS) > 0) { AddLogic("utils", new GameUtils()); }
            if ((selectedLogic & (uint)LogicEnum.GRAVITY) > 0) { AddLogic("gravity", new Gravity()); }
        }

        /// <summary>The player index for uniquely identifying a player in a game.</summary>
        public int index;
        /// <summary>Reference of the Game instance that owns this player object.</summary>
        public Game game;
        /// <summary>Whether or not this player is still alive in the game.</summary>
        public bool alive = true;
        /// <summary>Configuration for the logics.</summary>
        public LogicConfig logicConfig;
        private Dictionary<string, Logic> logics;
        private string[] logicPriority;

        /// <summary>
        /// Executes logics in the order of their priority.
        /// This is called every step in the game loop.
        /// </summary>
        /// <param name="dt">Seconds passed since last execution of logics.</param>
        public void Update(float dt)
        {
            for (int i = 0; i < logicPriority.Length; i++)
            {
                logics[logicPriority[i]].Update(dt);
            }
        }

        /// <summary>
        /// Get a logic by its key.
        /// </summary>
        /// <param name="key">The key of the logic to be retrieved.</param>
        /// <returns>The logic that matched the key.</returns>
        public Logic GetLogic(string key)
        {
            return logics[key];
        }

        /// <summary>
        /// Get keys of all registered logics.
        /// </summary>
        /// <returns>An array of logic keys.</returns>
        public string[] GetAllLogicKey()
        {
            return logicPriority;
        }

        /// <summary>
        /// Register a logic.
        /// </summary>
        /// <param name="key">A unique identifier for distinguishing from other type of logics.</param>
        /// <param name="logic">The logic instance to be registered.</param>
        /// <param name="priority">0 means it will be the first to be executed, 1 means executing it second, etc.<br/>
        /// -1 means executing it last.</param>
        /// <example>
        /// <code>
        /// // This will add the RendererLogic to the player
        /// // You can reference this logic by using the "renderer" key
        /// // And this logic will be the forth to be executed because priority is set to 3
        /// player.AddLogic("renderer", new RendererLogic(), 3);
        /// </code>
        /// </example>
        public void AddLogic(string key, Logic logic, int priority = -1)
        {
            logic.InjectParent(this);
            logic.UpdateConfig(logicConfig);
            logics.Add(key, logic);
            logicPriority = ArrayUtils.InsertAt(logicPriority, priority, key);
        }

        /// <summary>
        /// Unregister a logic.
        /// </summary>
        /// <param name="key">The key of the logic to be unregistered.</param>
        public void RemoveLogic(string key)
        {
            logics.Remove(key);
            logicPriority = ArrayUtils.RemoveFrom(logicPriority, key);
        }
    }
}
