using Artefact.InventorySystem;
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

        public override HitDamageRange HitDamage => Inventory.Weapon == null ? new HitDamageRange(1) : Inventory.Weapon.Damage;
        public override int Defense => Inventory.Defense;

        public PlayerEntity() : base(20)
        {
            Inventory = new Inventory();
        }

        public new void Damage(int amount)
        {
            Utils.WriteColor($"You have been dealt [red]{amount}[/] damage");
            base.Damage(amount);

            if (Health <= 0)
            {
                StateMachine.AddState(new GameOverState());
            }
        }

        public new void Heal(int amount)
        {
            Utils.WriteColor($"Healed by [green]{amount}[/] points!");
            base.Heal(amount);
        }
    }
}
