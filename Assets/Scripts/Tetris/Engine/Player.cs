using Selftris.Tetris.Engine.Logics;
using System.Collections.Generic;

namespace Selftris.Tetris.Engine {
    /// <summary>
    /// Contains all the logics and states of one player in a Tetris game.
    /// <br />
    /// Usually stored inside of <see cref="Game.players" />
    /// </summary>
    public class Player
    {
        public Player(int index, Game game, LogicConfig config)
        {
            this.index = index;
            this.game = game;
            this.config = config;
            logics = new Dictionary<string, Logic>();
            logicPriority = new string[] { };

            AddLogic("board", new Board());
            AddLogic("cs", new ControllerStates());
            AddLogic("utils", new GameUtils());
            AddLogic("gravity", new Gravity());

            foreach (Logic l in logics.Values)
            {
                l.InjectParent(this);
            }
        }

        public int index;
        public Game game;
        public bool alive = true;
        public LogicConfig config;
        private Dictionary<string, Logic> logics;
        private string[] logicPriority;

        /// <summary>
        /// Tells all logics of this player to calculate 
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public bool Update(float dt)
        {
            for (int i = 0; i < logicPriority.Length; i++)
            {
                logics[logicPriority[i]].Update(dt);
            }
            return alive;
        }

        public Logic GetLogic(string key)
        {
            return logics[key];
        }

        public string[] GetAllLogicKey()
        {
            return logicPriority;
        }

        public void AddLogic(string key, Logic logic, int priority = -1)
        {
            logic.InjectParent(this);
            logic.UpdateConfig(config);
            logics.Add(key, logic);
            logicPriority = Utils.InsertAt(logicPriority, priority, key);
        }

        public void RemoveLogic(string key)
        {
            logics.Remove(key);
            logicPriority = Utils.RemoveFrom(logicPriority, key);
        }
    }
}
