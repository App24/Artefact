using Artefact.CraftingSystem;
using Artefact.Entities;
using Artefact.InventorySystem;
using Artefact.Misc;
using Artefact.Settings;
using Artefact.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Artefact.Items.Fighting
{
    internal class LightingBolt : Item, IUsable
    {
        public bool IsUsable => GameSettings.InFight;

        public LightingBolt() : base("Lighting Bolt", "ZAP an enemy to death in one hit", new CraftData(new ItemData(ElectronItem, 10)))
        {
        }

        public bool OnUse()
        {
            List<EnemyEntity> enemyEntities = FightState.Enemies;

            EnemyEntity enemyToAttack = enemyEntities[0];
            if (enemyEntities.Count > 1)
            {
                Utils.WriteColor("Select an enemy to attack");
                int selection = Utils.GetSelection(enemyEntities.Map(e => e.EnemyType.ToString()).ToArray());
                enemyToAttack = enemyEntities[selection];
            }

            enemyToAttack.Damage(enemyToAttack.MaxHealth, true);
            return true;
        }
    }
}
