using Artefact.CraftingSystem;
using Artefact.DialogSystem;
using Artefact.Entities;
using Artefact.MapSystem;
using Artefact.MapSystem.Rooms;
using Artefact.Misc;
using Artefact.Settings;
using Artefact.States;
using System;
using System.Collections.Generic;
using System.Text;

namespace Artefact.Items.Fighting
{
    internal class MagnetItem : Item, IUsable
    {
        public bool IsUsable => true;

        public MagnetItem() : base("Magnet", "Erase all your search history and data from the hard drive")
        {
        }

        public bool OnUse()
        {
            HDDRoom hddRoom = (HDDRoom)Map.GetRoom(Location.HDD);
            if (!GameSettings.InFight && (Map.Player.Location != Location.HDD || (Map.Player.Location==Location.HDD && hddRoom.ErasedHardDrive)))
            {
                Utils.WriteColor("You can't use this here!");
                return false;
            }
            if (GameSettings.InFight)
            {
                List<EnemyEntity> enemyEntities = FightState.Enemies;

                enemyEntities.ForEach(enemy =>
                {
                    enemy.Damage(enemy.Health / 2);
                });
            }else if(Map.Player.Location == Location.HDD && !hddRoom.ErasedHardDrive)
            {
                Dialog.Speak(Character.Clippy, $"Oh look, the [{ColorConstants.USER_COLOR}]user[/]'s hard drive!");
                Dialog.Speak(Character.Clippy, "Get closer!");
                if (Utils.GetConfirmation("Do you want to get closer?"))
                {
                    Utils.WriteColor("*lash*");
                    Dialog.Speak(Character.Clippy, "[darkred]AAAAH");
                    Dialog.Speak(Character.Clippy, "You smashed me!");
                    Dialog.Speak(Character.Clippy, $"But you erased the [{ColorConstants.USER_COLOR}]user[/]'s hard drive, so I win in the end!");
                    Dialog.Speak(Character.Clippy, "[darkred]MWAHAHAHAHA");
                    hddRoom.ErasedHardDrive = true;
                }
                else
                {
                    Dialog.Speak(Character.Clippy, "You are not fun ¬_¬");
                }
            }
            Map.Player.Inventory.AddItem(new InventorySystem.ItemData(MagnetItem), false);

            return true;
        }
    }
}
