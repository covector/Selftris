using Selftris.Tetris.Engine.Configs;

namespace Selftris.Tetris.Engine.Logics
{
    /// <summary>
    /// Attach to a <see cref="Game"/> instance.<br />
    /// Each shared logic is responsible for one particular task for all the players.<br />
    /// E.g. GarbageDistribution logic is responsible for distributing garbage lines to different players.
    /// </summary>
    public abstract class SharedLogic
    {
        /// <summary>Reference to the game that owns this shared logic.</summary>
        protected Game game;

        /// <summary>
        /// The main function to perform that particular task it is responsible for.
        /// This is called every step in the game loop.
        /// </summary>
        /// <param name="dt">Seconds past since last execution of this function.</param>
        public abstract void Update(float dt);

        /// <summary>
        /// Link to the Game instance that owns this logic.
        /// </summary>
        /// <param name="parent">The Game instance that owns this logic.</param>
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
}
