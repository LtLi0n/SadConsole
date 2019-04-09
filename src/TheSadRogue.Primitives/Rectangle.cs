using System;
using System.Collections.Generic;
using System.Text;

namespace SadRogue.Primitives
{
    public struct Rectangle
    {
        public readonly int X;
        public readonly int Y;
        public readonly int Width;
        public readonly int Height;

        public Point Location => new Point(X, Y);

        public Point Center => new Point((Width / 2) + X, (Height / 2) + Y);

        public int Right => X + Width;

        public int Bottom => Y + Height;

        public Rectangle(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        //public Rectangle Inflate(int x, int y) => new Rectangle(X - x, Y - y, Width - x, Height - y);

        public bool Contains(Point position) => false;

        public bool Contains(Rectangle rectangle) => false;

        public static bool operator ==(Rectangle left, Rectangle right) => false;
        public static bool operator !=(Rectangle left, Rectangle right) => false;
    }
}
