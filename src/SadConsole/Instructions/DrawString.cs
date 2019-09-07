#if XNA
using Microsoft.Xna.Framework;
#endif

using System;
using System.Runtime.Serialization;

using Console = SadConsole.Console;

namespace SadConsole.Instructions
{
    /// <summary>
    /// Draws a string to a console as if someone was typing.
    /// </summary>
    public class DrawString : InstructionBase
    {
        private ColoredString _text;

        /// <summary>
        /// Gets or sets the text to print.
        /// </summary>
        public ColoredString Text
        {
            get => _text;
            set => _text = value ?? throw new Exception($"{nameof(Text)} can't be null.");
        }

        /// <summary>
        /// Gets or sets the position on the console to write the text.
        /// </summary>
        public Point Position { get; set; }

        /// <summary>
        /// Represents the cursor used in printing. Use this for styling and printing behavior.
        /// </summary>
        public Cursor Cursor { get; set; }

        private CellSurface _target;
        private double _timeElapsed = 0d;
        private double _timePerCharacter = 0d;
        private string _textCopy;
        private short _textIndex;
        private bool _started = false;
        private Point _tempLocation;

        /// <summary>
        /// Draws a string on the specified surface.
        /// </summary>
        /// <param name="target">The target surface to use.</param>
        /// <param name="text">The text to print.</param>
        public DrawString(CellSurface target, ColoredString text)
        {
            _target = target;
            Cursor = new Cursor();
            Text = text;
        }

        /// <summary>
        /// Draws a string on the surface passed to <see cref="Update(Console, TimeSpan)"/>.
        /// </summary>
        /// <param name="text"></param>
        public DrawString(ColoredString text)
        {
            Cursor = new Cursor();
            Text = text;
        }

        /// <summary>
        /// Draws a string on the surface passed to <see cref="Update(Console, TimeSpan)"/>. <see cref="Text"/> must be set manually.
        /// </summary>
        public DrawString()
        {
            Cursor = new Cursor();
            Text = new ColoredString();
        }

        /// <inheritdoc />
        public override void Update(Console console, TimeSpan delta)
        {
            if (!_started)
            {
                _started = true;
                _textCopy = Text.ToString();
                _textIndex = 0;

                _target ??= console;

                Cursor.AttachSurface(_target);
                Cursor.DisableWordBreak = true;

                if (_textCopy.Length == 0)
                {
                    IsFinished = true;
                    base.Update(console, delta);
                    return;
                }

                _tempLocation = Position;
            }


            _timeElapsed += Global.GameTimeElapsedUpdate;
            if (_timeElapsed >= Text[_textIndex].Speed)
            {
                _timeElapsed = 0d;
                Cursor.Position = _tempLocation;

                if(_textIndex < Text.Count - 1)
                {
                    var textToPrint = Text.SubString(_textIndex, 1);
                    Cursor.Print(textToPrint);
                    _textIndex++;
                }
                else
                {
                    IsFinished = true;
                }

                _tempLocation = Cursor.Position;
            }

            base.Update(console, delta);
        }

        /// <inheritdoc />
        public override void Repeat()
        {
            _started = false;
            _textIndex = 0;

            base.Repeat();
        }
    }
}
