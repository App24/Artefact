using Artefact.Commands;
using Artefact.Commands.FightCommands;
using Artefact.Commands.Misc;
using Artefact.DialogSystem;
using Artefact.Entities;
using Artefact.InventorySystem;
using Artefact.Items;
using Artefact.MapSystem;
using Artefact.Misc;
using Artefact.Saving;
using Artefact.Settings;
using Artefact.StorySystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Artefact.States
{
    class FightState : State
    {
        public static List<EnemyEntity> Enemies { get; private set; }

        public const float RUN_PROBABILITY = 0.1f;

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
            if (CanHaveRunCommand())
                commandHandler.AddCommand(new RunCommand());
#if DEBUG
            commandHandler.AddCommand(new KillCommand());
#endif
            #endregion

            CommandHandler.Instance = commandHandler;

            Utils.WriteColor($"[{ColorConstants.BAD_COLOR}]{Enemies.Count}[/] enem{(Enemies.Count == 1 ? "y" : "ies")} appear{(Enemies.Count == 1 ? "s" : "")}!");
            foreach (EnemyEntity enemy in Enemies)
            {
                Utils.WriteColor($"Type: [{ColorConstants.ENEMY_COLOR}]{enemy.EnemyType}[/]");
                Utils.WriteColor($"Health: [{ColorConstants.BAD_COLOR}]{enemy.Health}[/]");
                Utils.WriteColor($"Level: [{ColorConstants.BAD_COLOR}]{enemy.Level}[/]");
                Utils.WriteColor($"Hit Damage: [{ColorConstants.BAD_COLOR}]{enemy.HitDamage * enemy.HitModifierLevel}[/]");
                Utils.WriteColor($"Defense: [{ColorConstants.BAD_COLOR}]{enemy.Defense}[/]");
                if (!string.IsNullOrEmpty(enemy.ASCIIRepresentation))
                    Utils.WriteColor(enemy.ASCIIRepresentation);
                Console.WriteLine();
            }
        }

        public override void Update()
        {
            Utils.WriteColor($"[{ColorConstants.CHARACTER_COLOR}]{GameSettings.PlayerName}[/] Health: {Utils.CreateHealthBar(Map.Player)}");
            Enemies.ForEach(e => Utils.WriteColor($"[{ColorConstants.ENEMY_COLOR}]{e.EnemyType}[/] Health: {Utils.CreateHealthBar(e, barColor: ColorConstants.BAD_COLOR)}"));

            if (PlayerTurn())
            {

                EnemiesTurn();

            }

            CheckForDeaths();

            if (Story.Step == Story.CPU_STEP)
            {
                if (Map.Player.Health < 20)
                {
                    Dialog.Speak(Character.Clippy, "Here, you need this!");
                    Map.Player.Heal(20 - Map.Player.Health);
                }
            }

            Console.WriteLine();

            if (Enemies.Count <= 0)
            {
                StateMachine.RemoveState();
                Utils.WriteColor($"[{ColorConstants.GOOD_COLOR}]Defeated all enemies!");
#if !BYPASS
                Thread.Sleep((int)GlobalSettings.TextSpeed * 15);
#endif
            }
        }

        bool PlayerTurn()
        {
            Map.Player.DefenseModifier = 1f;
            while (true)
            {
                Utils.WriteColor($"Are you going to {CommandHandler.Instance.GetCommands().Map(c => $"[{ColorConstants.COMMAND_COLOR}]{c.Name}[/]").Join(", ")}?");
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
                    }
                    break;
                case Move.SweepAttack:
                    {
                        int damagePerEnemy = (int)Math.Ceiling(Map.Player.GetRandomDamage() / (float)Enemies.Count);

                        Enemies.ForEach(enemy =>
                        {
                            enemy.Damage(damagePerEnemy);
                        });
                    }
                    break;
                case Move.Run:
                    {
                        Random random = new Random();
                        float probability = (float)random.NextDouble();
                        if (probability < RUN_PROBABILITY)
                        {
                            StateMachine.RemoveState();
                            Utils.WriteColor($"[{ColorConstants.GOOD_COLOR}]You successfully ran away!");
#if !BYPASS
                Thread.Sleep((int)GlobalSettings.TextSpeed * 15);
#endif
                            return false;
                        }
                        else
                        {
                            Utils.WriteColor($"[{ColorConstants.BAD_COLOR}]You were unable to run away!");
                        }
                    }
                    break;
                case Move.InstaKill:
                    {
                        Enemies.ForEach(enemy =>
                        {
                            enemy.Damage(enemy.MaxHealth);
                        });
                    }
                    break;
            }
            return true;
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
                            Utils.WriteColor($"[{ColorConstants.ENEMY_COLOR}]{enemy.EnemyType}[/] attacked!");
                            Map.Player.Damage(enemy.GetRandomDamage());
                        }
                        break;
                    case Move.Defend:
                        {
                            Utils.WriteColor($"[{ColorConstants.ENEMY_COLOR}]{enemy.EnemyType}[/] is defending!");
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
                Utils.WriteColor($"Killed [{ColorConstants.ENEMY_COLOR}]{enemy.EnemyType}");
                Random random = new Random();
                foreach (ItemDropData itemDropData in enemy.ItemDrops)
                {
                    float per = (float)random.NextDouble();
                    if (per < itemDropData.Chance)
                    {
                        int amount = random.Next((int)itemDropData.Min, (int)itemDropData.Max + 1);
                        Map.Player.Inventory.AddItem(new ItemData(itemDropData.Item, amount), true);
                    }
                }
                int xpAmount = random.Next(enemy.XPRange.Min, enemy.XPRange.Min + 1);
                Map.Player.AddXP(xpAmount);
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
                Dialog.Speak(Character.Clippy, $"Use the command [{ColorConstants.COMMAND_COLOR}]USE[/] to utilise those potions!");
                Story.Step = Story.EMPTY_STEP;
            }
        }

        bool CanHaveRunCommand()
        {
            return Story.Step != Story.CPU_STEP;
        }
    }
}
