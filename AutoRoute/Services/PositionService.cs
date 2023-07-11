using AutoRoute.Models;
using AutoRoute.Views;
using System.Net;
using System.Text.Json;

namespace AutoRoute.Services
{

    public interface IPositionService
    {
        Task<object> CreatePosition(PositionRequestView data);
    }
    public class PositionService : IPositionService
    {
        private HttpClient _client;
        private IInMemorySettings _inMemorySettings;

        public PositionService(IInMemorySettings inMemorySettings)
        {
            _client = new HttpClient();
            _inMemorySettings = inMemorySettings;
        }

        public async Task<object> CreatePosition(PositionRequestView data)
        {
            var url = new Uri($"http://api.positionstack.com/v1/forward?access_key=89132d4939c47c6461873109c15fab50&query={data.Name}");

            var response = await _client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var str =  await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<PositionList>(str);

                if (result is not null && result.Data.Any())
                {
                    _inMemorySettings.AddPosition(result.Data.FirstOrDefault());
                }

                return result;
            }

            throw new Exception();
        }
    }
}
