﻿using System;

namespace Artefact.MapSystem.Rooms
{
    [Serializable]
    abstract class Room
    {
        public Location Location { get; }
        public Location North { get; }
        public Location South { get; }
        public Location East { get; }
        public Location West { get; }

        public bool VisitedBefore { get; private set; }

        public Room(Location location, Location north = Location.None, Location south = Location.None, Location east = Location.None, Location west = Location.None)
        {
            Location = location;
            North = north;
            South = south;
            East = east;
            West = west;
        }

        public abstract void OnInteract();

        public void OnEnter()
        {
            if (!VisitedBefore)
            {
                OnEnterFirst();
                VisitedBefore = true;
            }
            OnEnterRoom();
        }

        protected abstract void OnEnterRoom();
        protected abstract void OnEnterFirst();
    }
}
