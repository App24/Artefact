using System;

namespace Artefact.Commands.Misc
{
    internal class CommandException : Exception
    {
        public CommandException(string message) : base(message)
        {

        }
    }
}
