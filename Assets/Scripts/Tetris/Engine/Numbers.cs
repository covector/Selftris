using System;

namespace Selftris.Tetris.Engine
{
    public struct Position
    {
        public int x;
        public int y;

        public Position(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        // Addition
        public static Position operator +(Position a, Position b)
        {
            return new Position(a.x + b.x, a.y + b.y);
        }

        // Subtraction
        public static Position operator -(Position a, Position b)
        {
            return new Position(a.x - b.x, a.y - b.y);
        }

        // Multiplication
        public static Position operator *(Position a, int scale)
        {
            return new Position(a.x * scale, a.y * scale);
        }

        public static Position operator *(int scale, Position b)
        {
            return b * scale;
        }

        // Division
        public static Position operator /(Position a, int scale)
        {
            return new Position(a.x / scale, a.y / scale);
        }

        public static Position operator /(int scale, Position b)
        {
            return b / scale;
        }

        public override string ToString()
        {
            return string.Format("({0}, {1})", x, y);
        }
    }

    public struct Rotation
    {
        public int value;

        public Rotation(int value)
        {
            if (value < 0 || value > 3) { throw new ArgumentOutOfRangeException("value", value, "The value must be between 0 and 3"); }
            this.value = value;
        }

        // Addition
        public static Rotation operator +(Rotation a, int integer)     // Assumes -3 < integer < 3
        {
            return new Rotation((a.value + integer + 4) % 4);
        }

        public static Rotation operator +(int integer, Rotation b)     // Assumes -3 < integer < 3
        {
            return b + integer;
        }

        public static Rotation operator +(Rotation a, Rotation b)
        {
            return new Rotation((a.value + b.value + 4) % 4);
        }

        // Subtraction
        public static Rotation operator -(Rotation a, int bVal)     // Assumes -3 < integer < 3
        {
            return new Rotation((a.value - bVal + 4) % 4);
        }
        public static Rotation operator -(int integer, Rotation b)     // Assumes -3 < integer < 3
        {
            return b - integer;
        }

        public static Rotation operator -(Rotation a, Rotation b)
        {
            return new Rotation((a.value - b.value + 4) % 4);
        }

        public static implicit operator int(Rotation d) => d.value;

        public override string ToString()
        {
            return value.ToString();
        }
    }
}
