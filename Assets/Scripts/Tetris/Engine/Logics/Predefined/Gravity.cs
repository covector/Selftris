using Selftris.Tetris.Engine.Configs;
using Selftris.Tetris.Engine.Types;

namespace Selftris.Tetris.Engine.Logics.Predefined
{
    /// <summary>
    /// Responsible for moving the current piece downward incrementally.
    /// </summary>
    /// <remarks>Dependent logics: <see cref="Board"/>, <see cref="ControllerStates"/>, <see cref="GameUtils"/>, <see cref="SharedPredefined.PiecesManager"/></remarks>
    class Gravity : Logic
    {
        public Gravity() { }

        /// <summary>Normal drop speed of the piece.</summary>
        private const float dropSpeed = 1f;

        /// <summary>Drop speed is multiplied by this value when soft dropping.</summary>
        private const float softDropMultiplier = 4f;

        /// <summary>Cooldown timer to see when to drop. i.e. The piece will drop when this timer reached 1.</summary>
        private float cooldown = 0f;

        /// <summary>Whether it dropped during this game step.</summary>
        public bool didDrop = false;

        /// <summary>Whether there is no obstacle beneath the piece.</summary>
        public bool canDrop = false;

        /// <summary>
        /// Increment the cooldown timer and move the piece down if the timer has reached 1.
        /// </summary>
        /// <param name="dt">Time pasted since last execution.</param>
        public override void Update(float dt) {
            didDrop = false;
            canDrop = false;

            if (DropCheck())    // Check to see if there is obstacle beneath
            {
                canDrop = true;

                // Increase cooldown
                float dropMultiplier = ((ControllerStates)GetLogic("cs")).softDropCS ? softDropMultiplier : 1f;
                cooldown += dt * dropSpeed * dropMultiplier;

                // Check if cooldown has been reached
                if (cooldown > 1f)
                {
                    ((Board)GetLogic("board")).curPiecePos.y -= 1;
                    didDrop = true;
                    cooldown -= 1f;
                }
            }
            else
            {
                cooldown = 0f;
            }
        }

        public override void UpdateConfig(LogicConfig config) { }

        /// <summary>
        /// Check if the block can go down 1 unit without intersection
        /// </summary>
        /// <returns>Whether there is obstacle beneath.</returns>
        private bool DropCheck()
        {
            Board board = (Board)GetLogic("board");
            return ((GameUtils)GetLogic("utils")).CheckOccupation(board.curPieceID, board.curPiecePos - new Position(0, 1), board.curPieceRot);
        }
    }
}
