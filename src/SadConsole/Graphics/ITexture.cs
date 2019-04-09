using System;
using System.Collections.Generic;
using System.Text;

namespace SadConsole.Graphics
{
    public interface ITexture
    {
        int Height { get; }
        int Width { get; }
    }

    public static class Textures
    {
        public static ITexture Load(string path)
        {

        }
    }
}
