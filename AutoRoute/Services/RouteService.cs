using AutoRoute.Controllers;
using AutoRoute.Models;
using AutoRoute.Views;
using System.Net;
using System.Text.Json;

namespace AutoRoute.Services
{
    public interface IRouteService
    {
        Task<object> GetRoute();
    }
    public class RouteService : IRouteService
    {
        private HttpClient _client;
        private HttpClientHandler _handler;
        private readonly IInMemorySettings _inMemorySettings;
        public RouteService(IInMemorySettings inMemorySettings)
        {
            _handler = new HttpClientHandler
            {
                Credentials = new NetworkCredential("mignnoni", "tricolor")
            };

            _client = new HttpClient(_handler);
            _inMemorySettings = inMemorySettings;
        }
        public async Task<object> GetRoute()
        {
            List<Position> positions = _inMemorySettings.GetPositions();

            positions.Add(new Position
            {
                Label = "Pedra Moura",
                Latitude = -29.657,
                Longitude = -50.5745
            });

            List<LocationView> list = GetList(positions);

            var url = new Uri("https://api.routexl.com/tour");

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var json = JsonSerializer.Serialize(list, options);

            var formData = new Dictionary<string, string>
            {
                {"locations", json}
            };

            var formContent = new FormUrlEncodedContent(formData);

            var response = await _client.PostAsync(url, formContent);

            var strResponse = await response.Content.ReadAsStringAsync();
            var sortedRoute = JsonSerializer.Deserialize<SortedRoute>(strResponse);

            if (response.IsSuccessStatusCode)
            {
                if (sortedRoute is not null)
                    _inMemorySettings.AddRoute(sortedRoute);

                _inMemorySettings.ClearPositions();
                return await response.Content.ReadAsStringAsync();
            }

            throw new Exception();
        }

        private static List<LocationView> GetList(List<Position> positions)
        {
            var list = new List<LocationView>();

            foreach (var position in positions)
            {
                list.Add(new LocationView
                {
                    Address = position.Label,
                    Lat = position.Latitude.ToString(),
                    Lng = position.Longitude.ToString(),
                });
            }

            return list;
        }
    }
}
