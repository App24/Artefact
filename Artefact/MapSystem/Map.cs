using Artefact.Entities;
using Artefact.FightSystem;
using Artefact.Misc;
using Artefact.Settings;
using Artefact.States;
using Artefact.StorySystem;
using System;
using System.Collections.Generic;
using System.Text;

namespace Artefact.MapSystem
{
    [Serializable]
    class Map
    {
        public const float ENCOUNTER_PROBABILITY = 1 / 30f;

        public static PlayerEntity Player { get { return Instance.player; } set { Instance.player = value; } }
        public static List<Entity> Entities { get { return Instance.entities; } set { Instance.entities = value; } }

        public static Map Instance { get; set; }

        PlayerEntity player;
        List<Entity> entities = new List<Entity>();

        static List<Room> rooms = new List<Room>()
        {
            new Room(Location.GPU, Location.CPU, east: Location.HDD),
            new Room(Location.CPU, south: Location.GPU, east: Location.RAM),
            new Room(Location.RAM, west: Location.CPU, south:Location.HDD),
            new Room(Location.HDD, west: Location.GPU, north: Location.RAM)
        };

        static string map = "\n" +
 "             _____________\n" +
 "             |     |     |\n" +
$"             | [{ColorConstants.LOCATION_COLOR}]{Location.CPU}[/] | [{ColorConstants.LOCATION_COLOR}]{Location.RAM}[/] |\n" +
 "            _|_____|_____|_\n" +
 "     N      |       |     |\n" +
$"     ^      |       | [{ColorConstants.LOCATION_COLOR}]{Location.HDD}[/] |\n" +
$"     |      |       |_____|\n" +
 "W <--+--> E |       |\n" +
$"     |      |  [{ColorConstants.LOCATION_COLOR}]{Location.GPU}[/]  |\n" +
 "     V      |       |\n" +
 "     S      |       |\n" +
 "            |       |\n" +
 "            |_______|\n";

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

        public static EnemyEntity SpawnEnemy(EnemyType enemyType, Location location)
        {
            EnemyEntity enemyEntity = new EnemyEntity(enemyType);
            enemyEntity.Location = location;
            Entities.Add(enemyEntity);
            return enemyEntity;
        }

        public static void RemoveEntity(Entity entity)
        {
            Entities.Remove(entity);
        }

        public static string GetMapLocation(Location location)
        {
            return map.Replace(location.ToString(), new string[location.ToString().Length].Map(x => "x").Join(""));
        }

        public static bool MovePlayer(Direction direction, bool disableSpawn = false, bool forceSpawn = false)
        {
            (bool moved, Location location) = Move(direction, Player.Location);

            if (moved)
            {
                MovePlayer(location, disableSpawn, forceSpawn);

                return true;
            }
            return false;
        }

        public static void MovePlayer(Location location, bool disableSpawn = false, bool forceSpawn = false)
        {

            Player.Location = location;
            Utils.WriteColor($"Moved to: [{ColorConstants.LOCATION_COLOR}]{Player.Location}");

            if (location == Location.CPU && !GameSettings.CPUVisited)
            {
                Story.Step = Story.CPU_STEP;
                disableSpawn = true;
            }
            else if (location == Location.RAM && !GameSettings.RAMVisited)
            {
                Story.Step = Story.RAM_STEP;
                disableSpawn = true;
            }

            if (!disableSpawn || forceSpawn)
            {
                Random random = new Random();
                float probability = (float)random.NextDouble();
                if (probability < ENCOUNTER_PROBABILITY || forceSpawn)
                {
                    Fight.StartFight(location, new BattleParameters(EnemyType.Virus | EnemyType.Trojan | EnemyType.RansomWare));
                }
            }
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

        public static Room GetRoom(Location location)
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
        HDD,

        Room = 50
    }
}
