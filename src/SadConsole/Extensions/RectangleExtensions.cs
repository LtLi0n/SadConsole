using System.Runtime.CompilerServices;
using SadConsole;
using SadRogue.Primitives;

namespace SadConsole
{
    public static class RectangleExtensions
    {
        /// <summary>
        /// Centers a console with a viewport on a specific point.
        /// </summary>
        /// <param name="surface">The console viewport to adjust.</param>
        /// <param name="target">The location of the point to center on.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CenterViewPortOnPoint(this IConsoleViewPort surface, Point target) =>
            surface.ViewPort = surface.ViewPort.CenterOnPoint(target, surface.Width, surface.Height);

        /// <summary>
        /// Repositions the center of a rectangle to a specific point. Keeps the rectangle within the specified bounds.
        /// </summary>
        /// <param name="rect">The rectangle to center.</param>
        /// <param name="target">The point to center on.</param>
        /// <param name="maxWidth">The bounds of the rectangle cannot exceed this width.</param>
        /// <param name="maxHeight">The bounds of the rectangle cannot exceed this height.</param>
        /// <returns></returns>
        public static Rectangle CenterOnPoint(this Rectangle rect, Point target, int maxWidth, int maxHeight)
        {
            var newRect = rect.WithCenter(target);

            if (newRect.MaxExtentX >= maxWidth)
                newRect = newRect.WithX(newRect.X - newRect.MaxExtentX - maxWidth + 1);
            else if (newRect.X < 0)
                newRect = newRect.WithX(0);

            if (newRect.MaxExtentY >= maxHeight)
                newRect = newRect.WithY(newRect.Y - newRect.MaxExtentY - maxHeight + 1);
            else if (newRect.Y < 0)
                newRect = newRect.WithY(0);

            return newRect;
        }

        /// <summary>
        /// Converts a rectangle from cells to pixels.
        /// </summary>
        /// <param name="rect">The rectangle to work with.</param>
        /// <param name="font">The font used for translation.</param>
        /// <returns>A new rectangle in pixels.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle ToPixels(this Rectangle rect, Font font) =>
            new Rectangle(rect.Position * font.Size, rect.Size * font.Size);

        /// <summary>
        /// Converts a rectangle from cells to pixels.
        /// </summary>
        /// <param name="rect">The rectangle to work with.</param>
        /// <param name="cellWidth">The width of a cell used in converting.</param>
        /// <param name="cellHeight">The height of a cell used in converting.</param>
        /// <returns>A new rectangle in pixels.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle ToPixels(this Rectangle rect, int cellWidth, int cellHeight) =>
            new Rectangle(rect.X * cellWidth, rect.Y * cellHeight, rect.Width * cellWidth, rect.Height * cellHeight);

        /// <summary>
        /// Converts a rectangle from pixels to cells.
        /// </summary>
        /// <param name="rect">The rectangle to work with.</param>
        /// <param name="font">The font used for translation.</param>
        /// <returns>A new rectangle in cell coordinates.</returns>
        public static Rectangle ToConsole(this Rectangle rect, Font font) =>
            new Rectangle(rect.Position / font.Size, rect.Size / font.Size);

        /// <summary>
        /// Converts a rectangle from pixels to cells.
        /// </summary>
        /// <param name="rect">The rectangle to work with.</param>
        /// <param name="cellWidth">The width of a cell used in converting.</param>
        /// <param name="cellHeight">The height of a cell used in converting.</param>
        /// <returns>A new rectangle in cell coordinates.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle ToConsole(this Rectangle rect, int cellWidth, int cellHeight) =>
            new Rectangle(rect.X / cellWidth, rect.Y / cellHeight, rect.Width / cellWidth, rect.Height / cellHeight);
    }
}
