using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Collections;
using SadRogue.Primitives;

namespace SadConsole
{
    /// <summary>
    /// Extensions for the Gradient type.
    /// </summary>
    public static class GradientExtensions
    {
        /// <summary>
        /// Creates a <see cref="SadConsole.ColoredString"/> object using the current gradient.
        /// </summary>
        /// <param name="gradient">The gradient to convert to a colored string.</param>
        /// <param name="text">The text to use for the colored string.</param>
        /// <returns>A new colored string object.</returns>
        public static SadConsole.ColoredString ToColoredString(this Gradient gradient, string text)
        {
            SadConsole.ColoredString stringObject = new SadConsole.ColoredString(text);

            if (gradient.Stops.Length == 0)
                throw new global::System.IndexOutOfRangeException("The Gradient object does not have any gradient stops defined.");

            else if (gradient.Stops.Length == 1)
            {
                stringObject.SetForeground(gradient.Stops[0].Color);
                return stringObject;
            }

            float lerp = 1f / (text.Length - 1);
            float lerpTotal = 0f;

            stringObject[0].Foreground = gradient.Stops[0].Color;
            stringObject[text.Length - 1].Foreground = gradient.Stops[gradient.Stops.Length - 1].Color;

            for (int i = 1; i < text.Length - 1; i++)
            {
                lerpTotal += lerp;
                int counter;
                for (counter = 0; counter < gradient.Stops.Length && gradient.Stops[counter].Stop < lerpTotal; counter++) ;

                counter--;
                counter = (int)MathHelpers.Clamp(counter, 0, gradient.Stops.Length - 2);

                float newLerp = (gradient.Stops[counter].Stop - (float)lerpTotal) / (gradient.Stops[counter].Stop - gradient.Stops[counter + 1].Stop);

                stringObject[i].Foreground = Color.Lerp(gradient.Stops[counter].Color, gradient.Stops[counter + 1].Color, newLerp);
            }

            return stringObject;
        }
    }
}
