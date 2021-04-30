namespace Selftris.Tetris.Engine
{
    /// <summary>
    /// Stores information of a particular piece type.
    /// </summary>
    public struct Piece
    {
        /// <summary>The spawning position.</summary>
        public Position initPos;
        /// <summary>
        /// Store the information of all the blocks in a piece is relative to its position.
        /// The 1st index corresponds to the rotation of the piece.
        /// The 2nd index corresponds to the index of the block in the piece.
        /// </summary>
        /// <example>
        /// <code>
        /// piece.occupationTable[
        /// </code>
        /// </example>
        public Position[][] occupationTable;
        /// <summary></summary>
        public Position[][] wallKickTable;
    }
}
