#if XNA
using Microsoft.Xna.Framework;
#endif

using SadConsole.Effects;

namespace SadConsole
{
    ///<summary>Represents a single character that has a foreground/background colors.</summary>
    public class ColoredGlyph : Cell
    {
        public char GlyphCharacter
        {
            get => (char)Glyph;
            set => Glyph = value;
        }

        public float Speed { get; set; } = 0.2f;
        public ICellEffect Effect { get; set; }


        ///<summary>Creates a new colored glyph based on provided cell's foreground, background, glyph and mirror state values.</summary>
        public ColoredGlyph(Cell cell) : base(cell.Foreground, cell.Background, cell.Glyph, cell.Mirror)
        {
            GlyphCharacter = (char)cell.Glyph;
        }

        public ColoredGlyph() : base(Color.White, Color.Black, 0) { }
        public ColoredGlyph(int glyph) : base(Color.White, Color.Black, glyph) { }
        public ColoredGlyph(int glyph, Color foreground, Color background) : base(foreground, background, glyph) { }

        ///<summary>Creates a new copy of this cell appearance.</summary>
        ///<returns>The cloned cell appearance.</returns>
        public new ColoredGlyph Clone() =>
            new ColoredGlyph()
            {
                Foreground = Foreground,
                Background = Background,
                Effect = Effect != null ?
                            (Effect.CloneOnApply ? Effect.Clone() : Effect)
                            : null,
                GlyphCharacter = GlyphCharacter,
                Speed = Speed,
                Mirror = Mirror
            };
    }
}
