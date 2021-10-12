using Artefact.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Artefact.MapSystem
{
    [Serializable]
    class Map
    {
        public static PlayerEntity Player { get { return Instance.player; } set { Instance.player = value; } }
        public static List<Entity> Entities { get { return Instance.entities; } set { Instance.entities = value; } }

        public static Map Instance { get; set; }

        PlayerEntity player;
        List<Entity> entities = new List<Entity>();

        public Map()
        {
            if (Instance == null)
                Instance = this;
        }

        public static void SpawnPlayer()
        {
            Player = new PlayerEntity();
            Player.Location = Location.GPU;
        }

        public static void SpawnEnemy(EnemyType enemyType, Location location)
        {
            EnemyEntity enemyEntity = new EnemyEntity(enemyType);
            enemyEntity.Location = location;
            Entities.Add(enemyEntity);
        }

        public static void RemoveEntity(Entity entity)
        {
            Entities.Remove(entity);
        }
    }

    enum Location
    {
        GPU,
        CPU
    }
}
