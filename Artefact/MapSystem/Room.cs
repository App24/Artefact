namespace Artefact.MapSystem
{
    struct Room
    {
        public Room(Location location, Location north = Location.None, Location south = Location.None, Location east = Location.None, Location west = Location.None)
        {
            Location = location;
            North = north;
            South = south;
            East = east;
            West = west;
        }

        public Location Location { get; }
        public Location North { get; }
        public Location South { get; }
        public Location East { get; }
        public Location West { get; }
    }
}
