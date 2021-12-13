using Artefact.Entities;
using Artefact.FightSystem;
using Artefact.MapSystem.Rooms;
using Artefact.Misc;
using System;
using System.Collections.Generic;

namespace Artefact.MapSystem
{
    [Serializable]
    internal class Map
    {
        public const float ENCOUNTER_PROBABILITY = 1 / 25f;

        public static PlayerEntity Player { get { return Instance.player; } set { Instance.player = value; } }

        public static Map Instance { get; set; }

        private PlayerEntity player;

        public static List<Room> Rooms { get { return Instance.rooms; } }

        private List<Room> rooms = new List<Room>()
        {
            new GPURoom(),
            new CPURoom(),
            new RAMRoom(),
            new HDDRoom(),
            new PSURoom(),
            new UserRoom()
        };
        private static string map = "\n" +
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
            return enemyEntity;
        }

        /// <summary>
        /// Get map in string format
        /// </summary>
        /// <param name="location">The location where the entity is</param>
        /// <returns>A map with the <paramref name="location"/> replaced with x's</returns>
        public static string GetMapLocation(Location location)
        {
            return map.Replace(location.ToString(), new string[location.ToString().Length].Map(x => "x").Join(""));
        }

        /// <summary>
        /// Move the player in a certain direction
        /// </summary>
        /// <param name="direction">The direction to move the player</param>
        /// <param name="disableSpawn">Whether to disable spawning when the player moves</param>
        /// <param name="forceSpawn">Whether to force spawning when the player moves</param>
        /// <returns>If the player moved or not</returns>
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
                currentRoom.OnEnter(ref disableSpawn);
            }

            if (!disableSpawn || forceSpawn)
            {
                Random random = new Random();
                float probability = (float)random.NextDouble();
                if (probability < ENCOUNTER_PROBABILITY || forceSpawn)
                {
                    EnemyType spawnableEnemies = Room.DefaultSpawnableEnemies;
                    if (currentRoom != null) spawnableEnemies = currentRoom.SpawnableEnemies;
                    Fight.StartFight(location, new BattleParameters(spawnableEnemies));
                }
            }
        }

        /// <summary>
        /// Get the location that is depending on the direction
        /// </summary>
        /// <param name="direction">Direction to go</param>
        /// <param name="currentLocation">Current location</param>
        /// <param name="nextLocation">The next location</param>
        /// <returns>Whether it was able to move or not</returns>
        public static bool Move(Direction direction, Location currentLocation, out Location nextLocation)
        {
            Room currentRoom = GetRoom(currentLocation);
            nextLocation = Location.None;
            if (currentRoom == null) return false;
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

    internal enum Direction
    {
        North,
        South,
        East,
        West
    }

    internal enum Location
    {
        None,
        GPU,
        CPU,
        RAM,
        HDD,
        PSU,

        Room
    }
}
