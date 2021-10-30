using Artefact.InventorySystem;
using Artefact.Items.Equipables;
using Artefact.Misc;
using Artefact.States;
using System;
using System.Collections.Generic;
using System.Text;

namespace Artefact.Entities
{
    [Serializable]
    class PlayerEntity : Entity
    {
        public Inventory Inventory { get; }

        public override IntRange HitDamage => Inventory.Weapon == null ? new IntRange(3, 5) : Inventory.Weapon.Damage;

        public override int Defense
        {
            get
            {
                int defense = 0;
                foreach (KeyValuePair<ArmorType, ArmorItem> keyValuePair in Inventory.Armor)
                {
                    defense += keyValuePair.Value.Defense;
                }
                return defense;
            }
        }

        public PlayerEntity() : base(100)
        {
            Inventory = new Inventory();
        }

        public new void Damage(int amount)
        {
            Utils.WriteColor($"You have been dealt [{ColorConstants.BAD_COLOR}]{amount}[/] damage");
            base.Damage(amount);

            if (Health <= 0)
            {
                StateMachine.AddState(new GameOverState());
            }
        }

        public new void Heal(int amount)
        {
            Utils.WriteColor($"Healed by [{ColorConstants.GOOD_COLOR}]{amount}[/] points!");
            base.Heal(amount);
        }

        public new void AddXP(int amount)
        {
            Utils.WriteColor($"You earnt [{ColorConstants.XP_COLOR}]{amount}[/] xp");
            AddXP(amount, IncreaseLevel);
        }

        public new void IncreaseLevel()
        {
            Utils.WriteColor($"[{ColorConstants.XP_COLOR}]You leveled up");
            base.IncreaseLevel();
        }
    }
}
