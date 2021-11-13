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
                currentRoom.OnInteract();
            }
            else
            {
                Utils.WriteColor($"[{ColorConstants.BAD_COLOR}]There is nothing here to interact with!");
            }
        }
    }
}
