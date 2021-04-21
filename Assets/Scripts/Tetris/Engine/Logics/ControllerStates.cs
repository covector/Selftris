namespace Selftris.Tetris.Engine.Logics
{
    class ControllerStates : Logic
    {
        public ControllerStates() {}

        private Player player;

        public int horizontalCS { get; set; }
        public bool softDropCS { get; set; }
        public bool hardDropCS { get; set; }

        public override void Update(float dt)
        {

        }
    }
}
