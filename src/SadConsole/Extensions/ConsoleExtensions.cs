using System.Numerics;
using SadRogue.Primitives;

namespace SadConsole
{
    public static class ConsoleExtensions
    {
        public static System.Numerics.Matrix3x2 GetPositionTransform(this Console surface, Point position, bool usePixelPositioning = false)
        {
            var worldLocation = usePixelPositioning ? position : new Point(position.X * surface.Font.Size.X, position.Y * surface.Font.Size.Y);
            return System.Numerics.Matrix3x2.CreateTranslation(new Vector2(worldLocation.X, worldLocation.Y));
        }
    }
}
