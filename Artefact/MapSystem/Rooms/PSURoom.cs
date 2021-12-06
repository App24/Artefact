using Artefact.DialogSystem;
using Artefact.Entities;
using Artefact.FightSystem;
using Artefact.InventorySystem;
using Artefact.Items;
using Artefact.Misc;
using Artefact.Settings;
using System;
using System.Threading;

namespace Artefact.MapSystem.Rooms
{
    [Serializable]
    internal class PSURoom : Room
    {

        bool battledElectricity;
        public bool RepairedPSU { get; private set; }

        public override EnemyType SpawnableEnemies => base.SpawnableEnemies | EnemyType.Electricity;

        public PSURoom() : base(Location.PSU, south: Location.CPU)
        {

        }

        public override void OnInteract(ref bool sucess)
        {
            sucess = true;
            if (battledElectricity)
            {
                if (!RepairedPSU)
                {
                    Dialog.Speak(Character.Clippy, "You've broke it, see?!");
                    Dialog.Speak(Character.Clippy, $"Seems like it can be easily fixed, all we need is 10 [{ColorConstants.ITEM_COLOR}]{Item.ElectronItem}[/]");
                    if (!Map.Player.Inventory.HasItem(new ItemData(Item.ElectronItem, 10)))
                    {
                        Dialog.Speak(Character.Clippy, "You do not have enough, we can come back later");
                        return;
                    }

                    Dialog.Speak(Character.Clippy, $"YIPPY! You got enough [{ColorConstants.ITEM_COLOR}]{Item.ElectronItem}[/] to repair");

                    if (Utils.GetCharacterConfirmation(Character.Clippy, $"You sure you want to use 10 [{ColorConstants.ITEM_COLOR}]{Item.ElectronItem}[/] to repair the PSU?"))
                    {
                        Map.Player.Inventory.RemoveItem(new ItemData(Item.ElectronItem, 10));

                        Utils.WriteColor("[yellow]*BZZT*");
                        Thread.Sleep((int)GlobalSettings.TextSpeed);
                        Utils.WriteColor("[yellow]*BZZT*");
                        Thread.Sleep((int)GlobalSettings.TextSpeed);
                        Utils.WriteColor("[yellow]*BZZT*");

                        Dialog.Speak(Character.Clippy, "We repaired it, yay!");

                        RepairedPSU = true;
                    }
                    else
                    {
                        Dialog.Speak(Character.Player, "We can come back later!");
                    }
                }
                else
                {
                    sucess = false;
                }
            }
            else
            {
                Dialog.Speak(Character.Clippy, "I TOLD YOU NOT TO TOUCH IT!!!");
                Fight.StartFight(Map.Player.Location, new BattleParameters(EnemyType.Electricity, new IntRange(1, 1), 1), new FightParameters(true, true, () =>
                {
                    battledElectricity = true;
                    Dialog.Speak(Character.Clippy, "Oh, look what you have gone and done, it is broken now");
                }));
            }
        }

        protected override void OnEnterFirst(ref bool disableSpawn)
        {
            disableSpawn = true;
            Dialog.Speak(Character.Clippy, $"Here is the [{ColorConstants.LOCATION_COLOR}]PSU[/]");
            Dialog.Speak(Character.Clippy, "Be careful around here, you touch the power supply and ZAP, you are fried into a crisp");
            Dialog.Speak(Character.Clippy, $"Just like I wish I could do to the [{ColorConstants.USER_COLOR}]user[/]");
            Utils.ClearPreviousLines();
        }

        protected override void OnEnterRoom(ref bool disableSpawn)
        {

        }
    }
}
