namespace Selftris.Tetris.Engine
{
    public abstract class SharedLogic
    {
        protected Game game;

        public abstract void Update(float dt);

        public void InjectParent(Game parent)
        {
            game = parent;
        }

        /// <summary>
        /// Reference a shared logics.
        /// </summary>
        /// <param name="key">The key of the shared logic to be reference.</param>
        /// <returns>The shared logic being referenced.</returns>
        protected SharedLogic GetSharedLogic(string key)
        {
            return game.GetSharedLogic(key);
        }

        /// <summary>
        /// Cache the configuration from the Game instance that owns this shared logic.
        /// </summary>
        /// <param name="config">The configuration to be cached.</param>
        public abstract void UpdateConfig(SharedLogicConfig config);
    }

    /// <summary>
    /// Stores the configuration for all the shared logics.
    /// </summary>
    public readonly struct SharedLogicConfig
    {
        public SharedLogicConfig(float dropSpeed)
        {
            this.dropSpeed = dropSpeed;
        }

        public float dropSpeed { get; }
    }
}
