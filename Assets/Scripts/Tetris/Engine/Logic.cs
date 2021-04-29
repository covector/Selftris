namespace Selftris.Tetris.Engine
{
    public abstract class Logic
    {
        protected Player player;

        public abstract void Update(float dt);

        public void InjectParent(Player parent)
        {
            player = parent;
        }

        protected Logic GetLogic(string key)
        {
            return player.GetLogic(key);
        }
    }
}
