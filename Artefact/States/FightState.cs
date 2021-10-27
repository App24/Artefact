using Artefact.Commands.FightCommands;
using Artefact.Commands.Misc;
using Artefact.DialogSystem;
using Artefact.Entities;
using Artefact.InventorySystem;
using Artefact.MapSystem;
using Artefact.Misc;
using Artefact.Settings;
using Artefact.StorySystem;
using System;
using System.Collections.Generic;
using System.Text;

namespace Artefact.States
{
    class FightState : State
    {
        List<EnemyEntity> enemies;

        public FightState(params EnemyEntity[] enemies)
        {
            this.enemies = new List<EnemyEntity>(enemies);
        }

        public override void Init()
        {
            Console.Clear();

            CommandHandler commandHandler = new CommandHandler();

            #region Commands
            commandHandler.AddCommand(new AttackCommand());
            #endregion

            CommandHandler.Instance = commandHandler;

            Utils.WriteColor($"[red]{enemies.Count}[/] enem{(enemies.Count == 1 ? "y" : "ies")} appear{(enemies.Count == 1 ? "s" : "")}!");
            foreach (EnemyEntity enemy in enemies)
            {
                Utils.WriteColor($"Type: [blue]{enemy.EnemyType}[/]\nHealth: [red]{enemy.Health}[/]\nHit Damage: [red]{enemy.HitDamage}[/]");
                if(!string.IsNullOrEmpty(enemy.ASCIIRepresentation))
                Utils.WriteColor(enemy.ASCIIRepresentation);
                Console.WriteLine();
            }
        }

        public override void Update()
        {
            CommandHandler.Instance.OnCommand();
            KillEnemy(enemies[0]);

            if (enemies.Count <= 0)
            {
                StateMachine.RemoveState();
                Utils.WriteColor("[green]Defeated all enemies!");
            }
        }

        void KillEnemy(EnemyEntity enemy)
        {
            if (enemies.Contains(enemy))
            {
                Utils.WriteColor($"Killed [blue]{enemy.EnemyType}");
                Random random = new Random();
                foreach(ItemDropData itemDropData in enemy.ItemDrops)
                {
                    float per = (float)random.NextDouble();
                    if (per <= itemDropData.Chance)
                    {
                        int amount = random.Next((int)itemDropData.Min, (int)itemDropData.Max);
                        Map.Player.Inventory.AddItem(new ItemData(itemDropData.Item, amount), true);
                    }
                }
                enemy.Kill();
                enemies.Remove(enemy);
            }
        }

        public override void Remove()
        {
            GameSettings.EnableCommands = true;
            if (Story.Step == Story.CPU_STEP)
            {
                Dialog.Speak(Character.Clippy, $"Phew, that was a close one!");
                Story.Step = Story.EMPTY_STEP;
            }
        }
    }
}
