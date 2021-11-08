using Artefact.DialogSystem;
using Artefact.Entities;
using Artefact.FightSystem;
using Artefact.GenderSystem;
using Artefact.InventorySystem;
using Artefact.Items;
using Artefact.Misc;
using Artefact.Settings;
using Artefact.StorySystem;
using System;
using System.Collections.Generic;
using System.Text;

namespace Artefact.MapSystem.Rooms
{
    [Serializable]
    class CPURoom : Room
    {

        public CPURoom() : base(Location.CPU, south: Location.GPU, east: Location.RAM, north: Location.PSU)
        {

        }

        public override void OnInteract()
        {

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
                Map.Player.Inventory.AddItem(new ItemData(Item.SmallHealthPotion, 3), true);
                Dialog.Speak(Character.Clippy, $"Use the command [{ColorConstants.COMMAND_COLOR}]USE[/] to utilise those potions!");
            }));
        }

        protected override void OnEnterRoom(ref bool disableSpawn)
        {

        }
    }
}
