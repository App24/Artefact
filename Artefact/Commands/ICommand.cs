using System.Collections.Generic;

namespace Artefact.Commands
{
    internal interface ICommand
    {
        string Name { get; }
        string[] Aliases { get; }

        bool HasArguments { get; }
        string NoArgsResponse { get; }

        string Description { get; }

        void OnRun(List<string> args);
    }
}
