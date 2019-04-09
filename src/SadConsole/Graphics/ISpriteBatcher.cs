using System;
using System.Collections.Generic;
using System.Text;

namespace SadConsole.Graphics
{
    public interface ISpriteBatcher
    {
        void Begin(ITexture target);
        void Draw();
        void End();
    }
}
