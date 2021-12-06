using Artefact.MapSystem;
using Artefact.MapSystem.Rooms;
using Artefact.Misc;
using System.Collections.Generic;

namespace Artefact.Commands
{
    internal class InteractCommand : ICommand
    {
        public string Name => "interact";

        public string[] Aliases => new string[] { };

        public bool HasArguments => false;

        public string NoArgsResponse => "";

        public string Description => "Interact with stuff in the room";

        public void OnRun(List<string> args)
        {
            Room currentRoom = Map.GetRoom(Map.Player.Location);
            if (currentRoom != null)
            {
                bool success = false;
                currentRoom.OnInteract(ref success);
                if (!success) Utils.WriteColor("There is [red]nothing[/] to interact in this room!");
            }
            else
            {
                Utils.WriteColor($"[{ColorConstants.BAD_COLOR}]There is nothing here to interact with in this room!");
            }
        }
    }
}
