using System.Text.Json.Serialization;

namespace AutoRoute.Views
{
    public class LocationView
    {
        [JsonPropertyName("address")]
        public string Address { get; set; }
        [JsonPropertyName("lat")]
        public string Lat { get; set; }
        [JsonPropertyName("lng")]
        public string Lng { get; set; }
    }
}
