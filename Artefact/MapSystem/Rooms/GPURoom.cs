using Artefact.DialogSystem;
using Artefact.Misc;
using System;

namespace Artefact.MapSystem.Rooms
{
    [Serializable]
    internal class GPURoom : Room
    {
        public bool GPUStarted { get; private set; }

        public GPURoom() : base(Location.GPU, Location.CPU, east: Location.HDD)
        {

        }

        public override void OnInteract(ref bool success)
        {
            if (!GPUStarted)
            {
                success = true;
                Dialog.Speak(Character.Clippy, $"The [{ColorConstants.LOCATION_COLOR}]GPU[/] does not seem to be working properly");
                Dialog.Speak(Character.Clippy, $"Maybe a little force will fix it");
                while (true)
                {
                    if(!Utils.GetConfirmation("Want to use force on it?"))
                    {
                        Dialog.Speak(Character.Player, "Maybe some time later...");
                        break;
                    }
                    Random random = new Random();
                    if (random.Next(5) <= 1)
                    {
                        GPUStarted = true;
                        break;
                    }
                    else
                    {
                        Dialog.Speak(Character.Clippy, "Didn't work, try again!");
                    }
                }

                if (GPUStarted)
                {
                    Utils.WriteColor("[darkgray]WRHHHRHHH");
                    Dialog.Speak(Character.Clippy, "You fixed it! YAY!");
                }
            }
        }

        protected override void OnEnterFirst(ref bool disableSpawn)
        {

        }

        protected override void OnEnterRoom(ref bool disableSpawn)
        {

        }
    }
}
