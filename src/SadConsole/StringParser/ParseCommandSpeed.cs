using System;
using System.Collections.Generic;
using System.Text;

namespace SadConsole.StringParser
{
    public class ParseCommandSpeed : ParseCommandBase
    {
        //[c:speed sleep:50 char_speed:5]

        public float SpeedPerCharacter { get; set; }

        public ParseCommandSpeed(string parameters)
        {
            string[] parts = parameters.Split(new char[] { ':' }, 3);
            SpeedPerCharacter = float.Parse(parts[0]);
            CommandType = CommandTypes.Speed;
        }

        public override void Build(
            ref ColoredGlyph glyphState, 
            ColoredGlyph[] glyphString, 
            int surfaceIndex, 
            CellSurface surface, 
            ref int stringIndex, 
            string processedString, 
            ParseCommandStacks commandStack)
        {
            glyphState.Speed = SpeedPerCharacter;
        }
    }
}
