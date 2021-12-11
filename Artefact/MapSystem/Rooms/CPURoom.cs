using Artefact.DialogSystem;
using Artefact.Entities;
using Artefact.FightSystem;
using Artefact.GenderSystem;
using Artefact.InventorySystem;
using Artefact.Items;
using Artefact.Misc;
using Artefact.Settings;
using System;
using System.Threading;

namespace Artefact.MapSystem.Rooms
{
    [Serializable]
    internal class CPURoom : Room
    {
        public bool ActivatedLever { get; private set; }

        public CPURoom() : base(Location.CPU, south: Location.GPU, east: Location.RAM, north: Location.PSU)
        {

        }

        public override void OnInteract(ref bool success)
        {
            if (!ActivatedLever)
            {
                success = true;
                Dialog.Speak(Character.Clippy, "There seems to be a lever over here, come check it out!");
                if (Utils.GetCharacterConfirmation(Character.Clippy, "Do you want to pull it?"))
                {
                    Utils.Type("....................");
                    if (!ContinueStory())
                    {
                        Utils.WriteColor("Nothing happened...");
                        Dialog.Speak(Character.Clippy, $"Maybe we have to [{ColorConstants.COMMAND_COLOR}]INTERACT[/] with the other rooms first?!");
                        return;
                    }

                    Utils.WriteColor("[darkgray]*CLANK*");
                    Dialog.Speak(Character.Clippy, $"[{ColorConstants.BAD_COLOR}]AAAAH");
                    Dialog.Speak(Character.Clippy, "The side panel, it fell");

                    Dialog.Speak(Character.Clippy, $"We are now able to go into the [{ColorConstants.USER_COLOR}]user[/]'s room, just go [{ColorConstants.WARNING_COLOR}]WEST[/]");

                    West = Location.Room;

                    ActivatedLever = true;
                }
            }
        }

        protected override void OnEnterFirst(ref bool disableSpawn)
        {
            disableSpawn = true;
            Dialog.Speak(Character.Clippy, $"This is the [{ColorConstants.LOCATION_COLOR}]CPU[/]!");
            Dialog.Speak(Character.Clippy, $"This is where everything the [{ColorConstants.USER_COLOR}]user[/] does is processed!");
            Dialog.Speak(Character.Clippy, "It is a marvel sight!");
            Dialog.Speak(Character.Clippy, $"But it seems to be turned off right now, which is odd, since the [{ColorConstants.USER_COLOR}]user[/] always has [{PronounType.Possessive_Determiner}] computer turned on!");
            Dialog.Speak(Character.Clippy, ".........................");
            Dialog.Speak(Character.Clippy, "Wha-what is that?!?");
            Dialog.Speak(Character.Clippy, "RUN, IT'S A TROJAN!!!");
#if !BYPASS
            Thread.Sleep((int)GlobalSettings.TextSpeed * 15);
#endif
            Fight.StartFight(Map.Player.Location, new BattleParameters(EnemyType.Trojan, new IntRange(1, 1), 1), new FightParameters(true, true, () =>
            {
                Dialog.Speak(Character.Clippy, $"Phew, that was a close one!");
                Dialog.Speak(Character.Clippy, "You should use these to regenerate your health!");
                Map.Player.Inventory.AddItem(new ItemData(Item.SmallHealthPotion, 3));
                Dialog.Speak(Character.Clippy, $"Use the command [{ColorConstants.COMMAND_COLOR}]USE[/] to utilise those potions!");
            }));
        }

        protected override void OnEnterRoom(ref bool disableSpawn)
        {

        }

        private bool ContinueStory()
        {
            GPURoom gpuRoom = (GPURoom)Map.GetRoom(Location.GPU);
            RAMRoom ramRoom = (RAMRoom)Map.GetRoom(Location.RAM);
            HDDRoom hddRoom = (HDDRoom)Map.GetRoom(Location.HDD);
            PSURoom psuRoom = (PSURoom)Map.GetRoom(Location.PSU);

#if !INSTA_CPU
            return ramRoom.RepairedRAM && psuRoom.RepairedPSU && gpuRoom.GPUStarted && hddRoom.ErasedHardDrive;
#else
            return true;
#endif
        }
    }
}
