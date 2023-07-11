using AutoRoute.Models;

namespace AutoRoute.Services
{
    public interface IInMemorySettings
    {
        void AddPosition(Position position);
        void AddRoute(SortedRoute route);
        List<Position> GetPositions();
        List<SortedRoute> GetRoutes();
        void ClearPositions();
    }
    public class InMemorySettings : IInMemorySettings
    {
        public List<Position> Positions { get; set; } = new List<Position>();
        public List<SortedRoute> Routes { get; set; } = new List<SortedRoute>();
        public InMemorySettings()
        {
            Positions.Add(new Position
            {
                Label = "Pedra Moura",
                Latitude = -29.657,
                Longitude = -50.5745
            });
        }

        public void AddPosition(Position position)
        {
            Positions.Add(position);
        }

        public List<Position> GetPositions()
        {
            return Positions;
        }

        public void AddRoute(SortedRoute route)
        {
            Routes.Add(route);
        }

        public List<SortedRoute> GetRoutes()
        {
            return Routes;
        }

        public void ClearPositions()
        {
            Positions.Clear();

            Positions.Add(new Position
            {
                Label = "Pedra Moura",
                Latitude = -29.657,
                Longitude = -50.5745
            });
        }
    }
}
