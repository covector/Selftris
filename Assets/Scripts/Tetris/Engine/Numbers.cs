namespace Selftris.Tetris.Engine
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

    /// <summary>
    /// Represents the current rotation of a piece as an integer between 0 and 3 inclusive.<br />
    /// 0 means rest rotation. 1, 2 and 3 means rotating it 90 deg anti-clockwisely 1, 2 and 3 times respectively.
    /// </summary>
    public struct Rotation
    {
        /// <summary>The internal value that stores the current rotation as int.</summary>
        public int value;

        /// <param name="value">The internal value that stores the current rotation as int.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">Thrown when <paramref name="value"/> is not between 0 and 3 inclusive.</exception>
        public Rotation(int value)
        {
            if (value < 0 || value > 3) { throw new System.ArgumentOutOfRangeException("value", value, "The value must be between 0 and 3"); }
            this.value = value;
        }

        /// <summary>
        /// Perform addition between a rotation and an int.<br />
        /// It is assumed that the int is between -3 and 3 inclusive.
        /// </summary>
        /// <param name="a">The rotation.</param>
        /// <param name="integer">The int.</param>
        /// <returns>The result of the addition.</returns>
        public static Rotation operator +(Rotation a, int integer)
        {
            return new Rotation((a.value + integer + 4) % 4);
        }

        /// <summary>
        /// Perform addition between a rotation and an int.<br />
        /// It is assumed that the int is between -3 and 3 inclusive.
        /// </summary>
        /// <param name="integer">The int.</param>
        /// <param name="b">The rotation.</param>
        /// <returns>The result of the addition.</returns>
        public static Rotation operator +(int integer, Rotation b)
        {
            return b + integer;
        }

        /// <summary>
        /// Perform addition between two rotations.
        /// </summary>
        /// <param name="a">The 1st rotation.</param>
        /// <param name="b">The 2nd rotation.</param>
        /// <returns>The result of the addition.</returns>
        public static Rotation operator +(Rotation a, Rotation b)
        {
            return new Rotation((a.value + b.value + 4) % 4);
        }

        /// <summary>
        /// Perform subtraction between a rotation and an int.<br />
        /// It is assumed that the int is between -3 and 3 inclusive.
        /// </summary>
        /// <param name="a">The rotation.</param>
        /// <param name="integer">The int.</param>
        /// <returns>The result of the subtraction.</returns>
        public static Rotation operator -(Rotation a, int integer)     // Assumes -3 < integer < 3
        {
            return new Rotation((a.value - integer + 4) % 4);
        }

        /// <summary>
        /// Perform subtraction between two rotations.
        /// </summary>
        /// <param name="a">The 1st rotation.</param>
        /// <param name="b">The 2nd rotation.</param>
        /// <returns>The result of the subtraction.</returns>
        public static Rotation operator -(Rotation a, Rotation b)
        {
            return new Rotation((a.value - b.value + 4) % 4);
        }

        public static implicit operator int(Rotation d) => d.value;

        /// <summary>
        /// Converting to a readable string.
        /// </summary>
        /// <returns>The string of the integer representing the rotation.</returns>
        public override string ToString()
        {
            return value.ToString();
        }
    }
}
