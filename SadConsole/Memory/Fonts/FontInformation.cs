namespace SadConsole
{
    public class FontInformation : IFontInformation
    {
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public int Columns { get; set; } = 16;
        public int Rows { get; set; } = 16;
        public int GlyphWidth { get; set; } = 16;
        public int GlyphHeight { get; set; } = 16;
        public int GlyphPadding { get; set; } = 0;
        public int SolidGlyphIndex { get; set; } = 219;
    }
}
