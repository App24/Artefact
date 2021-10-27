using Artefact.Entities;
using Artefact.MapSystem;
using Artefact.Misc;
using Artefact.Settings;
using Artefact.States;
using System;
using System.Collections.Generic;
using System.Text;

namespace Artefact.FightSystem
{
    static class Fight
    {
        public static void StartFight(Location location, EnemyType allowedEnemies, int maxAmount=3)
        {
            Random random = new Random();
            int numEnemies = random.Next(maxAmount);
            List<EnemyEntity> enemies = new List<EnemyEntity>();
            List<EnemyType> allowedEnemyTypes=allowedEnemies.GetSetFlags();
            for(int i = 0; i < numEnemies+1; i++)
            {
                enemies.Add(Map.SpawnEnemy(allowedEnemyTypes[random.Next(allowedEnemyTypes.Count)], location));
            }
            StateMachine.AddState(new FightState(enemies.ToArray()), false);
        }
    }
}
