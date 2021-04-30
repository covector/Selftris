namespace Selftris.Tetris.Engine.Types
{
    /// <summary>
    /// Stores information of a particular piece type.
    /// </summary>
    public struct Piece
    {
        /// <summary>The spawning position.</summary>
        public Position initPos;

        /// <summary>
        /// Store the relative positions of all the blocks to the piece.
        /// The 1st index corresponds to the rotation of the piece.
        /// The 2nd index corresponds to the index of the block in the piece.
        /// </summary>
        public Position[][] occupationTable;

        /// <summary></summary>
        public Position[][] wallKickTable;
    }
}
