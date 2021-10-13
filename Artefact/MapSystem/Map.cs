using Artefact.Entities;
using Artefact.Misc;
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

        static string map = @$"
  _____________
  |     |     |
  | [darkcyan]{Location.CPU}[/] | [darkcyan]{Location.RAM}[/] |
__|_____|_____|__
|               |
|      [darkcyan]{Location.GPU}[/]      |
|_______________|
";

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

        public static string GetMapLocation(Location location)
        {
            return map.Replace(location.ToString(), new string[location.ToString().Length].Map(x=>"x").Join(""));
        }
    }

    enum Location
    {
        GPU,
        CPU,
        RAM,

        Room=50
    }
}
