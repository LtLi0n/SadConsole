using System;
using System.Collections.Generic;
using System.Reflection;
using SadRogue.Primitives;

namespace SadConsole
{
    /// <summary>
    /// Various extension methods to <see cref="Color"/> class.
    /// </summary>
    public static class ColorExtensions
    {
        /// <summary>
        /// Custom color mappings for the <see cref="FromParser(Color, string, out bool, out bool, out bool, out bool, out bool)"/> method.
        /// </summary>
        public static Dictionary<string, Color> ColorMappings = new Dictionary<string, Color>(16) { { "ansiblack", Color.AnsiBlack },
                                                                                                    { "ansired", Color.AnsiRed },
                                                                                                    { "ansigreen", Color.AnsiGreen },
                                                                                                    { "ansiyellow", Color.AnsiYellow },
                                                                                                    { "ansiblue", Color.AnsiBlue },
                                                                                                    { "ansimagenta", Color.AnsiMagenta },
                                                                                                    { "ansicyan", Color.AnsiCyan },
                                                                                                    { "ansiwhite", Color.AnsiWhite },
                                                                                                    { "ansiblackbright", Color.AnsiBlackBright },
                                                                                                    { "ansiredbright", Color.AnsiRedBright },
                                                                                                    { "ansigreenbright", Color.AnsiGreenBright },
                                                                                                    { "ansiyellowbright", Color.AnsiYellowBright },
                                                                                                    { "ansibluebright", Color.AnsiBlueBright },
                                                                                                    { "ansimagentabright", Color.AnsiMagentaBright },
                                                                                                    { "ansicyanbright", Color.AnsiCyanBright },
                                                                                                    { "ansiwhitebright", Color.AnsiWhiteBright } };

        

        

        /// <summary>
        /// Gets a random color.
        /// </summary>
        /// <param name="color">The color object to start with. Will be overridden.</param>
        /// <param name="random">A random object to get numbers from.</param>
        /// <returns>A new color.</returns>
        public static Color GetRandomColor(this Color color, Random random)
        {
            return new Color((byte)random.Next(255), (byte)random.Next(255), (byte)random.Next(255));
        }

        /// <summary>
        /// Returns a new Color using only the Red value of this color.
        /// </summary>
        /// <param name="color">Object instance.</param>
        /// <returns>A color with only the red channel set.</returns>
        public static Color RedOnly(this Color color) => new Color(color.R, 0, 0);

        /// <summary>
        /// Returns a new Color using only the Green value of this color.
        /// </summary>
        /// <param name="color">Object instance.</param>
        /// <returns>A color with only the green channel set.</returns>
        public static Color GreenOnly(this Color color) => new Color(0, color.G, 0);

        /// <summary>
        /// Returns a new Color using only the Blue value of this color.
        /// </summary>
        /// <param name="color">Object instance.</param>
        /// <returns>A color with only the blue channel set.</returns>
        public static Color BlueOnly(this Color color) => new Color(0, 0, color.B);

        /// <summary>
        /// Returns a new Color using only the Alpha value of this color.
        /// </summary>
        /// <param name="color">Object instance.</param>
        /// <returns>A color with only the alpha channel set.</returns>
        public static Color AlphaOnly(this Color color) => new Color((byte)0, (byte)0, (byte)0, color.A);

        /// <summary>
        /// Returns a new color with the red channel set to 0.
        /// </summary>
        /// <param name="color">Object instance.</param>
        /// <returns>A color with the red channel cleared.</returns>
        public static Color ClearRed(this Color color) => new Color((byte)0, color.G, color.B, color.A);

        /// <summary>
        /// Returns a new color with the green channel set to 0.
        /// </summary>
        /// <param name="color">Object instance.</param>
        /// <returns>A color with the green channel cleared.</returns>
        public static Color ClearGreen(this Color color) => new Color(color.R, (byte)0, color.B, color.A);

        /// <summary>
        /// Returns a new color with the blue channel set to 0.
        /// </summary>
        /// <param name="color">Object instance.</param>
        /// <returns>A color with the blue channel cleared.</returns>
        public static Color ClearBlue(this Color color) => new Color(color.R, color.G, (byte)0, color.A);

        /// <summary>
        /// Returns a new color with the alpha channel set to 0.
        /// </summary>
        /// <param name="color">Object instance.</param>
        /// <returns>A color with the alpha channel cleared.</returns>
        public static Color ClearAlpha(this Color color) => new Color(color.R, color.G, color.B, (byte)0);

        /// <summary>
        /// Returns a new color with the red channel set to 255.
        /// </summary>
        /// <param name="color">Object instance.</param>
        /// <returns>A color with the red channel fully set.</returns>
        public static Color FillRed(this Color color) => new Color((byte)255, color.G, color.B, color.A);

        /// <summary>
        /// Returns a new color with the green channel set to 255.
        /// </summary>
        /// <param name="color">Object instance.</param>
        /// <returns>A color with the green channel fully set.</returns>
        public static Color FillGreen(this Color color) => new Color(color.R, (byte)255, color.B, color.A);

        /// <summary>
        /// Returns a new color with the blue channel set to 255.
        /// </summary>
        /// <param name="color">Object instance.</param>
        /// <returns>A color with the blue channel fully set.</returns>
        public static Color FillBlue(this Color color) => new Color(color.R, color.G, (byte)255, color.A);

        /// <summary>
        /// Returns a new color with the alpha channel set to 255.
        /// </summary>
        /// <param name="color">Object instance.</param>
        /// <returns>A color with the alpha channel fully set.</returns>
        public static Color FillAlpha(this Color color) => new Color(color.R, color.G, color.B, (byte)255);

        /// <summary>
        /// Converts a color to the format used by <see cref="SadConsole.ParseCommandRecolor"/> command.
        /// </summary>
        /// <param name="color">The color to convert.</param>
        /// <returns>A string in this format R,G,B,A so for <see cref="Color.Green"/> you would get <code>0,128,0,255</code>.</returns>
        public static string ToParser(this Color color)
        {
            return $"{color.R},{color.G},{color.B},{color.A}";
        }

        /// <summary>
        /// Gets a color in the format of <see cref="SadConsole.ParseCommandRecolor"/>.
        /// </summary>
        /// <param name="color"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Color FromParser(this Color color, string value, out bool keepR, out bool keepG, out bool keepB, out bool keepA, out bool useDefault)
        {
            useDefault = false;
            keepR = false;
            keepG = false;
            keepB = false;
            keepA = false;

            var exception = new ArgumentException("Cannot parse color string");
            var r = color.R;
            var g = color.G;
            var b = color.B;
            var a = color.A;

            if (value.Contains(","))
            {
                string[] channels = value.Trim(' ').Split(',');

                if (channels.Length >= 3)
                {
                    byte colorValue;

                    // Red
                    if (channels[0] == "x")
                        keepR = true;
                    else if (byte.TryParse(channels[0], out colorValue))
                        r = colorValue;
                    else
                        throw exception;

                    // Green
                    if (channels[1] == "x")
                        keepG = true;
                    else if (byte.TryParse(channels[1], out colorValue))
                        g = colorValue;
                    else
                        throw exception;

                    // Blue
                    if (channels[2] == "x")
                        keepB = true;
                    else if (byte.TryParse(channels[2], out colorValue))
                        b = colorValue;
                    else
                        throw exception;

                    if (channels.Length == 4)
                    {
                        // Alpha
                        if (channels[3] == "x")
                            keepA = true;
                        else if (byte.TryParse(channels[3], out colorValue))
                            a = colorValue;
                        else
                            throw exception;
                    }
                    else
                        a = 255;

                    return new Color(r, g, b, a);
                }
                else
                    throw exception;
            }
            else if (value == "default")
            {
                useDefault = true;
                return color;
            }
            else
            {
                value = value.ToLower();

                if (ColorMappings.ContainsKey(value))
                    return ColorMappings[value];
                else
                {
                    // Lookup color in framework

                    TypeInfo colorType = typeof(Color).GetTypeInfo();

                    foreach (var item in colorType.DeclaredProperties)
                    {
                        if (item.Name.ToLower() == value)
                            return (Color)item.GetValue(null);
                    }

                    foreach (var item in colorType.DeclaredFields)
                    {
                        if (item.Name.ToLower() == value)
                            return (Color)item.GetValue(null);
                    }


                    throw exception;
                }
            }
        }
    }
}
