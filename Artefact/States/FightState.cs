using Artefact.Commands;
using Artefact.Commands.FightCommands;
using Artefact.Commands.Misc;
using Artefact.DialogSystem;
using Artefact.Entities;
using Artefact.InventorySystem;
using Artefact.Items;
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
        public static List<EnemyEntity> Enemies { get; private set; }

        public FightState(params EnemyEntity[] enemies)
        {
            Enemies = new List<EnemyEntity>(enemies);
        }

        public override void Init()
        {
            Console.Clear();

            CommandHandler commandHandler = new CommandHandler();

            #region Commands
            commandHandler.AddCommand(new UseCommand());
            commandHandler.AddCommand(new AttackCommand());
            commandHandler.AddCommand(new DefendCommand());
            commandHandler.AddCommand(new SweepAttackCommand());
            #endregion

            CommandHandler.Instance = commandHandler;

            Utils.WriteColor($"[red]{Enemies.Count}[/] enem{(Enemies.Count == 1 ? "y" : "ies")} appear{(Enemies.Count == 1 ? "s" : "")}!");
            foreach (EnemyEntity enemy in Enemies)
            {
                Utils.WriteColor($"Type: [blue]{enemy.EnemyType}[/]\nHealth: [red]{enemy.Health}[/]\nHit Damage: [red]{enemy.HitDamage}[/]\nDefense: [red]{enemy.Defense}[/]");
                if(!string.IsNullOrEmpty(enemy.ASCIIRepresentation))
                Utils.WriteColor(enemy.ASCIIRepresentation);
                Console.WriteLine();
            }
        }

        public override void Update()
        {
            Utils.WriteColor($"[cyan]{GameSettings.PlayerName}[/] Health: {Utils.CreateHealthBar(Map.Player)}");
            Enemies.ForEach(e => Utils.WriteColor($"[blue]{e.EnemyType}[/] Health: {Utils.CreateHealthBar(e, barcolor: ConsoleColor.Red)}"));

            //Player Turn
            PlayerTurn();

            //Enemies Turn
            EnemiesTurn();

            CheckForDeaths();

            if (Story.Step == Story.CPU_STEP)
            {
                if (Map.Player.Health < 5)
                {
                    Dialog.Speak(Character.Clippy, "Here, you need this!");
                    Map.Player.Heal(5 - Map.Player.Health);
                }
            }

            Console.WriteLine();

            if (Enemies.Count <= 0)
            {
                StateMachine.RemoveState();
                Utils.WriteColor("[green]Defeated all enemies!");
            }
        }

        void PlayerTurn()
        {
            Map.Player.DefenseModifier = 1f;
            while (true)
            {
                Utils.WriteColor($"Are you going to {CommandHandler.Instance.GetCommands().Map(c => $"[darkmagenta]{c.Name}[/]").Join(", ")}?");
                if (CommandHandler.Instance.OnCommand()) break;
            }
            Console.WriteLine();
            switch (Map.Player.Move)
            {
                case Move.Attack:
                    {
                        EnemyEntity enemyToAttack = Enemies[0];
                        if (Enemies.Count > 1)
                        {
                            Utils.WriteColor("Select an enemy to attack");
                            int selection = Utils.GetSelection(Enemies.Map(e => e.EnemyType.ToString()).ToArray());
                            enemyToAttack = Enemies[selection];
                        }

                        enemyToAttack.Damage(Map.Player.GetRandomDamage());
                    }
                    break;
                case Move.Defend:
                    {
                        Utils.WriteColor("You are defending!");
                        Map.Player.DefenseModifier = 1.5f;
                    }break;
                case Move.SweepAttack:
                    {
                        int damagePerEnemy = (int)Math.Ceiling(Map.Player.GetRandomDamage() / (float)Enemies.Count);

                        Enemies.ForEach(enemy =>
                        {
                            enemy.Damage(damagePerEnemy);
                        });
                    }
                    break;
            }
        }

        void EnemiesTurn()
        {
            Move[] allowedMoves = new Move[] { Move.Attack, Move.Defend };
            Enemies.ForEach(enemy =>
            {
                enemy.DefenseModifier = 0;
                Random random = new Random();
                Move move = allowedMoves[random.Next(allowedMoves.Length)];
                switch (move)
                {
                    case Move.Attack:
                        {
                            Utils.WriteColor($"[blue]{enemy.EnemyType}[/] attacked!");
                            Map.Player.Damage(enemy.GetRandomDamage());
                        }
                        break;
                    case Move.Defend:
                        {
                            Utils.WriteColor($"[blue]{enemy.EnemyType}[/] is defending!");
                            enemy.DefenseModifier = 1.25f;
                        }
                        break;
                }
            });
        }

        private void CheckForDeaths()
        {
            List<EnemyEntity> enemies = new List<EnemyEntity>(Enemies);

            enemies.ForEach(enemy =>
            {
                if (enemy.Health <= 0)
                    KillEnemy(enemy);
            });
        }

        void KillEnemy(EnemyEntity enemy)
        {
            if (Enemies.Contains(enemy))
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
                Enemies.Remove(enemy);
            }
        }

        public override void Remove()
        {
            GameSettings.EnableCommands = true;
            if (Story.Step == Story.CPU_STEP)
            {
                Dialog.Speak(Character.Clippy, $"Phew, that was a close one!");
                Dialog.Speak(Character.Clippy, "You should use these to regenerate your health!");
                Map.Player.Inventory.AddItem(new ItemData(Item.SmallHealthPotion, 3), true);
                Dialog.Speak(Character.Clippy, "Use the command [darkmagenta]USE[/] to utilise those potions!");
                Story.Step = Story.EMPTY_STEP;
            }
        }
    }
}
