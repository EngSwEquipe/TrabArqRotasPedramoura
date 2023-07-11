using System.Text.Json.Serialization;

namespace AutoRoute.Models
{
    public class Position
    {
        [JsonPropertyName("label")]
        public string Label { get; set; }
        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }
        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }

    }

    public class PositionList
    {
        [JsonPropertyName("data")]
        public List<Position> Data { get; set; }
    }
}
