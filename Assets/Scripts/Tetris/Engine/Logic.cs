namespace Selftris.Tetris.Engine
{
    /// <summary>
    /// Attach to a <see cref="Player"/> instance.<br />
    /// Each logic is responsible for one particular task.<br />
    /// E.g. Gravity logic is responsible for moving the current piece downward.
    /// </summary>
    public abstract class Logic
    {
        /// <summary>Reference to the player that owns this logic.</summary>
        protected Player player;

        /// <summary>
        /// The main function to perform that particular task it is responsible for.
        /// This is called every step in the game loop.
        /// </summary>
        /// <param name="dt">Seconds passed since last execution of this function.</param>
        public abstract void Update(float dt);

        /// <summary>
        /// Link the player variable to the Player instance that owns this logic.
        /// </summary>
        /// <param name="parent">The Player instance that owns this logic.</param>
        public void InjectParent(Player parent)
        {
            player = parent;
        }

        /// <summary>
        /// Reference other logics.
        /// </summary>
        /// <param name="key">The key of the logic to be reference.</param>
        /// <returns>The logic being referenced.</returns>
        protected Logic GetLogic(string key)
        {
            return player.GetLogic(key);
        }

        /// <summary>
        /// Cache the configuration from the Player instance that owns this logic.
        /// </summary>
        /// <param name="config">The configuration to be cached.</param>
        public abstract void UpdateConfig(LogicConfig config);
    }

    /// <summary>
    /// Stores the configuration for all the logics.
    /// </summary>
    public readonly struct LogicConfig
    {
        /// <param name="dropSpeed">Soft drop speed.</param>
        public LogicConfig(float dropSpeed)
        {
            this.dropSpeed = dropSpeed;
        }

        /// <summary>Soft drop speed.</summary>
        public float dropSpeed { get; }
    }
}
