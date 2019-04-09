using System;

namespace SadRogue.Primitives
{
    public struct Point
    {
        public readonly int X;
        public readonly int Y;

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void Deconstruct(out int x, out int y)
        {
            x = X;
            y = Y;
        }

        public static Point operator +(Point left, Point right) => new Point(left.X + right.X, left.Y + right.Y);
    }
}
