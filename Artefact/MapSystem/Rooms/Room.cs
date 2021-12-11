using Artefact.Entities;
using System;

namespace Artefact.MapSystem.Rooms
{
    [Serializable]
    internal abstract class Room
    {
        public Location Location { get; }
        public Location North { get; protected set; }
        public Location South { get; protected set; }
        public Location East { get; protected set; }
        public Location West { get; protected set; }

        public bool VisitedBefore { get; private set; }

        public static EnemyType DefaultSpawnableEnemies => EnemyType.Virus | EnemyType.Trojan | EnemyType.RansomWare | EnemyType.AdWare | EnemyType.SpyWare;

        public virtual EnemyType SpawnableEnemies => DefaultSpawnableEnemies;

        public Room(Location location, Location north = Location.None, Location south = Location.None, Location east = Location.None, Location west = Location.None)
        {
            Location = location;
            North = north;
            South = south;
            East = east;
            West = west;
        }

        public abstract void OnInteract(ref bool success);

        public void OnEnter(ref bool disableSpawn)
        {
            if (!VisitedBefore)
            {
                OnEnterFirst(ref disableSpawn);
                VisitedBefore = true;
            }
            OnEnterRoom(ref disableSpawn);
        }

        protected abstract void OnEnterRoom(ref bool disableSpawn);
        protected abstract void OnEnterFirst(ref bool disableSpawn);
    }
}
