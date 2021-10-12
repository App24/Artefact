using System;
using System.Collections.Generic;
using System.Text;

namespace Artefact.Commands.Misc
{
    class CommandException : Exception
    {
        public CommandException(string message) : base(message)
        {

        }
    }
}
