namespace Selftris.Tetris.Engine.Types
{
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
