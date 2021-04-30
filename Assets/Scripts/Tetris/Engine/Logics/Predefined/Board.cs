using Selftris.Tetris.Engine.Configs;
using Selftris.Tetris.Engine.Types;

namespace Selftris.Tetris.Engine.Logics.Predefined
{
    /// <summary>
    /// Responsible for storing the board current state and information of the current piece.
    /// </summary>
    /// <remarks>Dependent logics: <see cref="SharedPredefined.PiecesManager"/></remarks>
    public class Board : Logic
    {
        public Board() {}

        /// <summary>The height of the upper limit of the board. Pieces will not be stored if they lock at a spot above this.</summary>
        private const int ceilingHeight = 43;

        /// <summary>The current state of the board.<br />
        /// -1 means the position is empty. Otherwise it corresponds to the ID of the piece that occupied that position.</summary>
        public int[][] occupancy = new int[ceilingHeight][];

        /// <summary>Current piece ID.</summary>
        public int curPieceID;

        /// <summary>Current piece position</summary>
        public Position curPiecePos;

        /// <summary>Current piece rotation</summary>
        public Rotation curPieceRot;

        public override void Update(float dt) {}

        public override void UpdateConfig(LogicConfig config) { }

        /// <summary>
        /// Clearing the board to empty.
        /// </summary>
        public void Clear()
        {

        }
    }
}
