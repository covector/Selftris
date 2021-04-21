using Selftris.Tetris.Engine.Logics;
using System.Linq;

namespace Selftris.Tetris.Engine {
    public class Player
    {
        public Player(int index)
        {
            this.index = index;
            logics = new Logic[] { new Board() };
        }

        public int index;
        public bool alive = true;
        public Logic[] logics;

        public void AddLogics(Logic[] newLogics)
        {
            logics = logics.Concat(newLogics).ToArray();
        }

        public bool Update(float dt)
        {
            return alive;
        }
    }
}
