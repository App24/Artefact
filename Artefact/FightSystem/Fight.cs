using Artefact.Entities;
using Artefact.MapSystem;
using Artefact.Misc;
using Artefact.Saving;
using Artefact.Settings;
using Artefact.States;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Artefact.FightSystem
{
    internal static class Fight
    {
        public static void StartFight(Location location, BattleParameters battleParameters, FightParameters fightParameters = null)
        {
            SaveSystem.SaveGame(SaveSystem.CHECKPOINT_FILE);
            GameSettings.EnableCommands = false;
            Random random = new Random();
            int numEnemies = random.Next(battleParameters.MaxEnemyAmount) + 1;
            List<EnemyEntity> enemies = new List<EnemyEntity>();
            List<EnemyType> allowedEnemyTypes = battleParameters.AllowedEnemies.GetSetFlags().ToList();
            for (int i = 0; i < numEnemies; i++)
            {
                EnemyEntity enemy = Map.SpawnEnemy(allowedEnemyTypes[random.Next(allowedEnemyTypes.Count)], location);
                IntRange range = battleParameters.LevelRange;
                enemy.SetLevel(random.Next(range.Min, range.Max));
                enemies.Add(enemy);
            }
            StateMachine.AddState(new FightState(fightParameters, enemies.ToArray()), false);
        }
    }

    internal struct BattleParameters
    {
        public EnemyType AllowedEnemies { get; }
        public int MaxEnemyAmount { get; }
        public IntRange LevelRange { get; }

        public BattleParameters(EnemyType allowedEnemies, int maxAmount = 3, bool scaleUp = true)
        {
            MaxEnemyAmount = maxAmount;
            LevelRange = new IntRange(1, 10);
            if (scaleUp)
                LevelRange = new IntRange(Map.Player.Level, Map.Player.Level + 1);
            AllowedEnemies = allowedEnemies;
        }

        public BattleParameters(EnemyType allowedEnemies, IntRange levelRange, int maxAmount = 3) : this(allowedEnemies, maxAmount, false)
        {
            LevelRange = levelRange;
        }
    }
}
