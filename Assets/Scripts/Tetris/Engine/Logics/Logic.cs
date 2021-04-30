using Selftris.Tetris.Engine.Configs;

namespace Selftris.Tetris.Engine.Logics
{
    /// <summary>
    /// Attach to a <see cref="Player"/> instance.<br />
    /// Each logic is responsible for one particular task for one player.<br />
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
        /// Reference a shared logics.
        /// </summary>
        /// <param name="key">The key of the shared logic to be reference.</param>
        /// <returns>The shared logic being referenced.</returns>
        protected SharedLogic GetSharedLogic(string key)
        {
            return player.game.GetSharedLogic(key);
        }

        /// <summary>
        /// Cache the configuration from the Player instance that owns this logic.
        /// </summary>
        /// <param name="config">The configuration to be cached.</param>
        public abstract void UpdateConfig(LogicConfig config);
    }
}
