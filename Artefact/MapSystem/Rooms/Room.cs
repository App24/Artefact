using System;

namespace Artefact.MapSystem.Rooms
{
    abstract class Room
    {
        public Location Location { get; }
        public Location North { get; }
        public Location South { get; }
        public Location East { get; }
        public Location West { get; }

        public virtual bool StoryRelated { get; }
        public virtual int StoryStep { get; }
        public virtual bool VisitedBool { get; }

        public Room(Location location, Location north = Location.None, Location south = Location.None, Location east = Location.None, Location west = Location.None)
        {
            Location = location;
            North = north;
            South = south;
            East = east;
            West = west;
        }

        public abstract void OnInteract();
    }
}
