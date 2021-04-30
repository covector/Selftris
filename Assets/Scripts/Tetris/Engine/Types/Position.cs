namespace Selftris.Tetris.Engine.Types
{
    /// <summary>
    /// A simple 2D integer vector that can perform addition, subtraction and scaling.
    /// </summary>
    public struct Position
    {
        /// <summary>The horizontal component.</summary>
        public int x;
        /// <summary>The vertical component.</summary>
        public int y;

        /// <param name="x">The horizontal component.</param>
        /// <param name="y">The vertical component.</param>
        public Position(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// Perform vector addition.
        /// </summary>
        /// <param name="a">The 1st vector.</param>
        /// <param name="b">The 2nd vector.</param>
        /// <returns>The result of the addition.</returns>
        public static Position operator +(Position a, Position b)
        {
            return new Position(a.x + b.x, a.y + b.y);
        }

        /// <summary>
        /// Perform vector subtraction.
        /// </summary>
        /// <param name="a">The 1st vector.</param>
        /// <param name="b">The 2nd vector.</param>
        /// <returns>The result of the subtraction.</returns>
        public static Position operator -(Position a, Position b)
        {
            return new Position(a.x - b.x, a.y - b.y);
        }

        /// <summary>
        /// Perform vector scaling.
        /// </summary>
        /// <param name="a">The vector.</param>
        /// <param name="scale">The scalar.</param>
        /// <returns>The scaled vector.</returns>
        public static Position operator *(Position a, int scale)
        {
            return new Position(a.x * scale, a.y * scale);
        }

        /// <summary>
        /// Perform vector scaling.
        /// </summary>
        /// <param name="scale">The scalar.</param>
        /// <param name="b">The vector.</param>
        /// <returns>The scaled vector.</returns>
        public static Position operator *(int scale, Position b)
        {
            return b * scale;
        }

        /// <summary>
        /// Perform vector shrinking.
        /// </summary>
        /// <param name="a">The vector.</param>
        /// <param name="scale">The scalar.</param>
        /// <returns>The shrinked vector.</returns>
        public static Position operator /(Position a, int scale)
        {
            return new Position(a.x / scale, a.y / scale);
        }

        /// <summary>
        /// Converting to a readable string.
        /// </summary>
        /// <returns>The string in the format of "(x, y)".</returns>
        public override string ToString()
        {
            return string.Format("({0}, {1})", x, y);
        }
    }

}
