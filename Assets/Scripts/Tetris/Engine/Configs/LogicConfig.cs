namespace Selftris.Tetris.Engine.Configs
{
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
