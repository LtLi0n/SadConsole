using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SadConsole
{
    public static class SpriteBatchExtensions
    {
        public static void Draw(this SpriteBatch spriteBatch, Cell cell, Point position, Point size, Font font)
        {
            if (cell.Background != Color.Transparent)
            {
                Rectangle drawingRectangle = new Rectangle(position.X, position.Y, size.X, size.Y);

                spriteBatch.Draw(
                    font.Image,
                    drawingRectangle, 
                    font.GlyphRects[font.SolidGlyphIndex], 
                    cell.Background, 
                    0f, 
                    Vector2.Zero, 
                    SpriteEffects.None, 
                    0.3f);
            }
        }
    }
}
