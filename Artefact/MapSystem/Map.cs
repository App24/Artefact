using Artefact.Entities;
using Artefact.FightSystem;
using Artefact.MapSystem.Rooms;
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
        public const float ENCOUNTER_PROBABILITY = 1 / 25f;

        public static PlayerEntity Player { get { return Instance.player; } set { Instance.player = value; } }
        public static List<Entity> Entities { get { return Instance.entities; } set { Instance.entities = value; } }

        public static Map Instance { get; set; }

        PlayerEntity player;
        List<Entity> entities = new List<Entity>();

        public static List<Room> Rooms { get { return Instance.rooms; } }

        List<Room> rooms = new List<Room>()
        {
            new GPURoom(),
            new CPURoom(),
            new RAMRoom(),
            new HDDRoom(),
            new PSURoom()
        };

        static string map = "\n" +
 "     ___________\n" +
 "     |         |\n" +
$"     |   [{ColorConstants.LOCATION_COLOR}]{Location.PSU}[/]   |\n" +
 "     |_________|__________\n" +
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
            bool moved = Move(direction, Player.Location, out Location location);

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

            Room currentRoom = GetRoom(location);
            if (currentRoom != null)
            {
                currentRoom.OnEnter();
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

        public static bool Move(Direction direction, Location currentLocation, out Location nextLocation)
        {
            Room currentRoom = GetRoom(currentLocation);
            nextLocation = Location.None;
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
            return nextLocation != Location.None;
        }

        public static Room GetRoom(Location location)
        {
            return Rooms.Find(room => room.Location == location);
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
        PSU,

        Room = 50
    }
}
