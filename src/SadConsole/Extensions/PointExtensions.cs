using System.Runtime.CompilerServices;
using SadRogue.Primitives;
using System.Numerics;

namespace SadConsole
{
    public static class PointExtensions
    {
        /// <summary>
        /// Translates a console cell position to where it appears on the screen in pixels.
        /// </summary>
        /// <param name="point">The current cell position.</param>
        /// <param name="cellWidth">The width of a cell in pixels.</param>
        /// <param name="cellHeight">The height of a cell in pixels.</param>
        /// <returns>The pixel position of the top-left of the cell.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point ConsoleLocationToPixel(this Point point, int cellWidth, int cellHeight) =>
            new Point(point.X * cellWidth, point.Y * cellHeight);

        /// <summary>
        /// Translates a console cell position to where it appears on the screen in pixels.
        /// </summary>
        /// <param name="point">The current cell position.</param>
        /// <param name="font">The font to use in calculating the position.</param>
        /// <returns>The pixel position of the top-left of the cell.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point ConsoleLocationToPixel(this Point point, Font font) =>
            new Point(point.X * font.Size.X, point.Y * font.Size.Y);

        /// <summary>
        /// Translates a pixel to where it appears on a console cell.
        /// </summary>
        /// <param name="point">The current world position.</param>
        /// <param name="cellWidth">The width of a cell in pixels.</param>
        /// <param name="cellHeight">The height of a cell in pixels.</param>
        /// <returns>The cell position on the screen.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point PixelLocationToConsole(this Point point, int cellWidth, int cellHeight) =>
            new Point(point.X / cellWidth, point.Y / cellHeight);

        /// <summary>
        /// Translates a pixel to where it appears on a console cell.
        /// </summary>
        /// <param name="point">The current world position.</param>
        /// <param name="font">The font to use in calculating the position.</param>
        /// <returns>The cell position on the screen.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point PixelLocationToConsole(this Point point, Font font) => 
            new Point(point.X / font.Size.X, point.Y / font.Size.Y);

        /// <summary>
        /// Translates an x,y position to an array index.
        /// </summary>
        /// <param name="point">The position.</param>
        /// <param name="rowWidth">How many columns in a row.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ToIndex(this Point point, int rowWidth) => 
            Helpers.GetIndexFromPoint(point.X, point.Y, rowWidth);

        /// <summary>
        /// Translates an array index to a Point.
        /// </summary>
        /// <param name="index">The index in the array.</param>
        /// <param name="rowWidth">How many columns in a row.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point ToPoint(this int index, int rowWidth) => 
            Helpers.GetPointFromIndex(index, rowWidth);

        /// <summary>
        /// Gets the cell coordinates of the <paramref name="targetFont"/> based on a cell in the <paramref name="sourceFont"/>.
        /// </summary>
        /// <param name="point">The position of the cell in the <paramref name="sourceFont"/>.</param>
        /// <param name="sourceFont">The source font translating from.</param>
        /// <param name="targetFont">The target font translating to.</param>
        /// <returns>The position of the cell in the <paramref name="targetFont"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point TranslateFont(this Point point, Font sourceFont, Font targetFont) => 
            point.ConsoleLocationToPixel(sourceFont.Size.X, sourceFont.Size.Y).PixelLocationToConsole(targetFont.Size.X, targetFont.Size.Y);

        /// <summary>
        /// Converts a point to a vector.
        /// </summary>
        /// <param name="point">The point to convert.</param>
        /// <returns>A new vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 ToVector2(this Point point) =>
            new Vector2(point.X, point.Y);

        /// <summary>
        /// Creates a position matrix (in pixels) based on the position of a cell.
        /// </summary>
        /// <param name="position">The cell position.</param>
        /// <param name="cellSize">The size of the cell in pixels.</param>
        /// <param name="absolutePositioning">When true, indicates that the <paramref name="position"/> indicates pixels, not cell coordinates.</param>
        /// <returns>A matrix for rendering.</returns>
        public static Matrix3x2 ToPositionMatrix(this Point position, Point cellSize, bool absolutePositioning)
        {
            Vector2 worldLocation;

            if (absolutePositioning)
                worldLocation = new Vector2(position.X, position.Y);
            else
            {
                var point = position.ConsoleLocationToPixel(cellSize.X, cellSize.Y);
                worldLocation = new Vector2(point.X, point.Y);
            }

            return Matrix3x2.CreateTranslation(worldLocation);
        }
    }
}
