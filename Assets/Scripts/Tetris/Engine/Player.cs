using Selftris.Tetris.Engine.Logics;
using System.Collections.Generic;

namespace Selftris.Tetris.Engine {
    public class Player
    {
        public Player(int index)
        {
            this.index = index;
            logics = new Dictionary<string, Logic>();

            logics.Add("board", new Board());

            foreach (Logic l in logics.Values)
            {
                l.InjectParent(this);
            }
        }

        private int index;
        public bool alive = true;
        private Dictionary<string, Logic> logics;

        public void AddLogic(string key, Logic logic)
        {
            logic.InjectParent(this);
            logics.Add(key, logic);
        }

        public Logic GetLogic(string key)
        {
            return logics[key];
        }

        public bool Update(float dt)
        {
            foreach (Logic l in logics.Values)
            {
                l.Update(dt);
            }
            return alive;
        }
    }
}
