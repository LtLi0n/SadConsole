using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;

namespace SadConsole
{
    public sealed class Font : IFontInformation
    {
        public string Name { get;}
        public int Columns { get; }
        public int Rows { get; }
        public int GlyphPadding { get; }
        ///<summary>Essential rect sizes for drawing</summary>
        public ReadOnlyCollection<Rectangle> GlyphRects { get; }
        
        public string ImagePath { get; }
        public Texture2D Image { get; private set; }
        public bool Ready => Image != null;

        public int GlyphWidth { get; }
        public int GlyphHeight { get; }
        public int SolidGlyphIndex { get; }

        internal Font(IFontInformation fontInfo)
        {
            Name = fontInfo.Name;
            ImagePath = fontInfo.ImagePath;
            Columns = fontInfo.Columns;
            Rows = fontInfo.Rows;
            GlyphWidth = fontInfo.GlyphWidth;
            GlyphHeight = fontInfo.GlyphHeight;
            GlyphPadding = fontInfo.GlyphPadding;
            SolidGlyphIndex = fontInfo.SolidGlyphIndex;

            Rectangle[] glyphRects = new Rectangle[Columns * Rows];

            //populate glyph rects
            for(int y = 0; y < Rows; y++)
            {
                for(int x = 0; x < Columns; x++)
                {
                    glyphRects[y * Columns + x] = new Rectangle(x * GlyphWidth, y * GlyphHeight, GlyphWidth, GlyphHeight); 
                }
            }

            GlyphRects = new ReadOnlyCollection<Rectangle>(glyphRects);
        }

        ///<summary>Loads font from <see cref="ImagePath"/>.</summary>
        ///<param name="services">services containing <see cref="GraphicsDevice"/></param>
        public void Load(IServiceProvider services)
        {
            using FileStream fs = new FileStream(ImagePath, FileMode.Open, FileAccess.Read);

            GraphicsDevice graphicsDevice = services.GetService<GraphicsDevice>();
            Image = Texture2D.FromStream(graphicsDevice, fs);
        }

        ///<summary>Returns a rectangle that is positioned and sized based on the font and the cell position specified.</summary>
        ///<param name="x">The x-axis of the cell position.</param>
        ///<param name="y">The y-axis of the cell position.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Rectangle GetRenderRect(int x, int y)
        {
            return new Rectangle(x * GlyphWidth, y * GlyphHeight, GlyphWidth, GlyphHeight);
        }

        ///<summary>Gets the pixel position of a cell position based on the font size.</summary>
        ///<param name="x">The x coordinate of the position.</param>
        ///<param name="y">The y coordinate of the position.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Point GetWorldPosition(int x, int y)
        {
            return GetWorldPosition(new Point(x, y));
        }

        ///<summary>Gets the pixel position of a cell position based on the font size.</summary>
        ///<param name="position">The position to convert.</param>
        ///<returns>A new pixel point.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Point GetWorldPosition(Point position)
        {
            return new Point(position.X * GlyphWidth, position.Y * GlyphHeight);
        }
    }
}
