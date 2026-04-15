using Domain.Locations;
using Domain.Locations.ValueObjects;
using Domain.Positions;
using Domain.Positions.ValueObjects;

namespace WebApplication1
{
    public static class ImitatyaBazaDannya
    {
        private static readonly Dictionary<LocationId, Location> _locations = [];
        private static readonly Dictionary<PositionId, Position> _positions = [];

        public static void Add(Location entity)
        {
            if (_locations.ContainsKey(entity.Id))
                throw new ArgumentException("Позиция с ID уже существует");

            if (_locations.Any(l => l.Value.Name == entity.Name))
                throw new ArgumentException("Позиция с названием Name уже существует");
            _locations.Add(entity.Id, entity);
        }

        public static Location? GetById(LocationId Id)
        {
            _locations.TryGetValue(Id, out var position);
            return position;

        }

        public static IEnumerable<Location> GetAll()
        {
            return _locations.Values;
        }
        public static void Remove(LocationId Id)
        {
            _locations.Remove(Id);
        }


        public static void Add(Position entity)
        {
            if (_positions.ContainsKey(entity.Id))
                throw new ArgumentException("Position with this Id already exists");

            if (_positions.Any(p => p.Value.Name == entity.Name))
                throw new ArgumentException("Position with this Name already exists");

            _positions.Add(entity.Id, entity);
        }

        public static Position? GetById(PositionId Id)
        {
            _positions.TryGetValue(Id, out var position);
            return position;
        }

        public static IEnumerable<Location> GetAllLocations()
        {
            return _locations.Values;
        }

        public static IEnumerable<Position> GetAllPositions()
        {
            return _positions.Values;
        }

        public static void Remove(PositionId Id)
        {
            _positions.Remove(Id);
        }

        public static void InitializeStorage()
        {
            _locations.Clear();
            _positions.Clear();
        }
        public static void UpdateLocation (Location location)
        {
           _locations[location.Id] = location;
        }
        public static void UpdatePosition (Position position)
        {
            _positions[position.Id] = position;
        }

        public static bool RemoveLocation(LocationId Id)
        {
            return _locations.Remove(Id);
        }

        public static bool RemovePosition(PositionId Id)
        {
            return _positions.Remove(Id);
        }
        public static bool EntityArchive (Location location)
        {
           return !location.LifeTime.IsArchived;
        }
        public static bool EntityArchive (Position position)
        {
            return !position.LifeTime.IsArchived;
        }
    }
}

