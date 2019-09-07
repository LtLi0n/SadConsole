using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;
using System.Runtime.Serialization;

namespace SadConsole
{
    public struct Cell
    {
        public int Glyph { get; set; }
        public Color Foreground { get; set; }
        public Color Background { get; set; }

        public bool IsVisible { get; set; }

        public Cell(Color foreground)
        {
            Glyph = 0;
            Foreground = foreground;
            Background = Color.Black;

            IsVisible = true;
        }

        public Cell(Color foreground, Color background)
        {
            Glyph = 0;
            Foreground = foreground;
            Background = background;

            IsVisible = true;
        }

        public Cell(int glyph, Color foreground, Color background)
        {
            Glyph = glyph;
            Foreground = foreground;
            Background = background;

            IsVisible = true;
        }

        ///<summary>Copies the visual appearance to the specified cell. This includes foreground, background, glyph, and mirror effect.</summary>
        ///<param name="cell">The target cell to copy to.</param>
        public void CopyAppearanceTo(Cell cell)
        {
            cell.Foreground = Foreground;
            cell.Background = Background;
            cell.Glyph = Glyph;
        }

        ///<summary>Sets the foreground, background, glyph, and mirror effect to the same as the specified cell.
        ///<param name="cell">The target cell to copy from.</param>
        public void CopyAppearanceFrom(Cell cell)
        {
            Foreground = cell.Foreground;
            Background = cell.Background;
            Glyph = cell.Glyph;
        }

        ///<summary>Sets the foreground, background, glyph values to default.</summary>
        public void SetDefault()
        {
            Foreground = Color.White;
            Background = Color.Black;
            Glyph = 0;
        }
    }
}
