namespace SadConsole.StringParser
{
    using System.Collections.Generic;
    
    /// <summary>
    /// A list of behaviors applied as a string is processed.
    /// </summary>
    public class ParseCommandStacks
    {
        public bool TurnOnEffects;

        public Stack<ParseCommandBase> Speed { get; private set; }
        public Stack<ParseCommandBase> Foreground { get; private set; }
        public Stack<ParseCommandBase> Background { get; private set; }
        public Stack<ParseCommandBase> Glyph { get; private set; }
        public Stack<ParseCommandBase> Mirror { get; private set; }
        public Stack<ParseCommandBase> Effect { get; private set; }
        public Stack<ParseCommandBase> All { get; set; }

        public ParseCommandStacks()
        {
            Speed = new Stack<ParseCommandBase>(4);
            Foreground = new Stack<ParseCommandBase>(4);
            Background = new Stack<ParseCommandBase>(4);
            Glyph = new Stack<ParseCommandBase>(4);
            Mirror = new Stack<ParseCommandBase>(4);
            Effect = new Stack<ParseCommandBase>(4);
            All = new Stack<ParseCommandBase>(10);
        }

        /// <summary>
        /// Adds a behavior to the <see cref="All"/> collection and the collection 
        /// based on the <see cref="ParseCommandBase.CommandType"/> type.
        /// </summary>
        /// <param name="command"></param>
        public void AddSafe(ParseCommandBase command)
        {
            switch (command.CommandType)
            {
                case CommandTypes.Speed:
                    Speed.Push(command);
                    //All.Push(command); hide from undo command
                    break;
                case CommandTypes.Foreground:
                    Foreground.Push(command);
                    All.Push(command);
                    break;
                case CommandTypes.Background:
                    Background.Push(command);
                    All.Push(command);
                    break;
                case CommandTypes.Mirror:
                    Mirror.Push(command);
                    All.Push(command);
                    break;
                case CommandTypes.Effect:
                    Effect.Push(command);
                    All.Push(command);
                    break;
                case CommandTypes.Glyph:
                    Glyph.Push(command);
                    All.Push(command);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Removes a command from the appropriate command stack and from the <see cref="All"/> stack.
        /// </summary>
        /// <param name="command">The command to remove</param>
        public void RemoveSafe(ParseCommandBase command)
        {
            List<ParseCommandBase> commands = null;

            // Get the stack we need to remove from
            switch (command.CommandType)
            {
                case CommandTypes.Speed:
                    if (Speed.Count != 0)
                        commands = new List<ParseCommandBase>(Speed);
                    break;
                case CommandTypes.Foreground:
                    if (Foreground.Count != 0)
                        commands = new List<ParseCommandBase>(Foreground);
                    break;
                case CommandTypes.Background:
                    if (Background.Count != 0)
                        commands = new List<ParseCommandBase>(Background);
                    break;
                case CommandTypes.Mirror:
                    if (Mirror.Count != 0)
                        commands = new List<ParseCommandBase>(Mirror);
                    break;
                case CommandTypes.Effect:
                    if (Effect.Count != 0)
                        commands = new List<ParseCommandBase>(Effect);
                    break;
                case CommandTypes.Glyph:
                    if (Glyph.Count != 0)
                        commands = new List<ParseCommandBase>(Glyph);
                    break;
                default:
                    return;
            }

            // If we have one, remove and restore stack
            if (commands != null && commands.Contains(command))
            {
                commands.Remove(command);
                commands.Reverse();

                switch (command.CommandType)
                {
                    case CommandTypes.Speed:
                        Speed = new Stack<ParseCommandBase>(commands);
                        break;
                    case CommandTypes.Foreground:
                        Foreground = new Stack<ParseCommandBase>(commands);
                        break;
                    case CommandTypes.Background:
                        Background = new Stack<ParseCommandBase>(commands);
                        break;
                    case CommandTypes.Mirror:
                        Mirror = new Stack<ParseCommandBase>(commands);
                        break;
                    case CommandTypes.Effect:
                        Effect = new Stack<ParseCommandBase>(commands);
                        break;
                    case CommandTypes.Glyph:
                        Glyph = new Stack<ParseCommandBase>(commands);
                        break;
                    default:
                        return;
                }
            }

            var all = new List<ParseCommandBase>(All);
            if (all.Contains(command))
            {
                all.Remove(command);
                all.Reverse();
                All = new Stack<ParseCommandBase>(all);
            }
        }
    }
}
