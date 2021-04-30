namespace Selftris.Tetris.Engine.Configs
{
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
