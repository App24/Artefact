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

        static List<Room> rooms = new List<Room>()
        {
            new Room(Location.GPU, Location.CPU),
            new Room(Location.CPU, south: Location.GPU, east: Location.RAM),
            new Room(Location.RAM, west: Location.CPU)
        };

        static string map = @$"
           _____________
           |     |     |
           | [darkcyan]{Location.CPU}[/] | [darkcyan]{Location.RAM}[/] |
          _|_____|_____|
    N     |       |
    ^     |       |
    |     |       |
W <- -> E |       |
    |     |  [darkcyan]{Location.GPU}[/]  |
    V     |       |
    S     |       |
          |       |
          |_______|
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

        public static (bool, Location) Move(Direction direction, Location currentLocation)
        {
            Room currentRoom = GetRoom(currentLocation);
            Location nextLocation = Location.None;
            switch (direction)
            {
                case Direction.North:
                    nextLocation = currentRoom.North;
                    break;
                case Direction.South:
                    nextLocation = currentRoom.South;
                    break;
                case Direction.East:
                    nextLocation = currentRoom.East;
                    break;
                case Direction.West:
                    nextLocation = currentRoom.West;
                    break;
            }
            return (nextLocation != Location.None, nextLocation);
        }

        static Room GetRoom(Location location)
        {
            return rooms.Find(room => room.Location == location);
        }
    }

    enum Direction
    {
        North,
        South,
        East,
        West
    }

    enum Location
    {
        None,
        GPU,
        CPU,
        RAM,

        Room=50
    }
}
