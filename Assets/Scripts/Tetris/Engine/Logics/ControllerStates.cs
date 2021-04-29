namespace Selftris.Tetris.Engine.Logics
{
    class ControllerStates : Logic
    {
        public ControllerStates() {}

        public int horizontalCS;
        public bool softDropCS = false;
        public bool hardDropCS;

        public override void Update(float dt) {}

        public void ResetToDefault()
        {
            softDropCS = false;
        }
    }
}
