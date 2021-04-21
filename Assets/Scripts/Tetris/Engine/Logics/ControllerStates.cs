namespace Selftris.Tetris.Engine.Logics
{
    class ControllerStates : Logic
    {
        public ControllerStates() {}

        public int horizontalCS { get; set; }
        public bool softDropCS { get; set; }
        public bool hardDropCS { get; set; }

        public override void Update(float dt)
        {

        }
    }
}
