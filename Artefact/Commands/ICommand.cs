using System;
using System.Collections.Generic;
using System.Text;

namespace Artefact.Commands
{
    interface ICommand
    {
        string Name { get; }
        string[] Aliases { get; }

        bool HasArguments { get; }
        string NoArgsResponse { get; }

        string Description { get; }

        void OnRun(List<string> args);
    }
}
