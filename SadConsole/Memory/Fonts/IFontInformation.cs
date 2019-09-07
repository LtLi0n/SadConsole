namespace SadConsole
{
    public interface IFontInformation
    {
        string Name { get; }
        string ImagePath { get; }
        int Columns { get; }
        int Rows { get; }
        int GlyphWidth { get; }
        int GlyphHeight { get; }
        int GlyphPadding { get; }
        int SolidGlyphIndex { get; }
    }
}
