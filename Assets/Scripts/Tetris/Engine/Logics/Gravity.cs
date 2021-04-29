namespace Selftris.Tetris.Engine.Logics
{
    class Gravity : Logic
    {
        public Gravity() { }

        private const float dropSpeed = 1f;
        private const float softDropMultiplier = 4f;
        private float cooldown = 0f;
        public bool didDrop = false;

        public override void Update(float dt) {
            didDrop = false;

            // check if dropping is allowed
            if (DropCheck())
            {
                // increase cooldown
                float dropMultiplier = ((ControllerStates)GetLogic("cs")).softDropCS ? softDropMultiplier : 1f;
                cooldown += dt * dropSpeed * dropMultiplier;

                // check if cooldown has been reached
                if (cooldown > 1f)
                {
                    ((Board)GetLogic("board")).curPiecePos.y -= 1;
                    didDrop = true;
                    cooldown -= 1f;
                }
            }
            else    // there must be something blocking beneath
            {
                cooldown = 0f;
            }
        }

        public override void UpdateConfig(LogicConfig config) { }

        private bool DropCheck() // check if the block can go down 1 unit without intersection
        {
            Board board = (Board)GetLogic("board");
            return ((GameUtils)GetLogic("utils")).CheckOccupation(board.curPieceID, board.curPiecePos - new Position(0, 1), board.curPieceRot);
        }
    }
}
