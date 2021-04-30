using Selftris.Tetris.Engine.Configs;

namespace Selftris.Tetris.Engine.Logics.Predefined
{
    class ControllerStates : Logic
    {
        public ControllerStates() {}

        public int horizontalCS;
        public bool softDropCS = false;
        public bool hardDropCS;

        public override void Update(float dt) {}

        public override void UpdateConfig(LogicConfig config) { }
        public void ResetToDefault()
        {
            softDropCS = false;
        }
    }
}
