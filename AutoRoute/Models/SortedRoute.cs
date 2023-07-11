using System.Text.Json.Serialization;

namespace AutoRoute.Models
{
    public class SortedRoute
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("count")]
        public int Count { get; set; }
    }
}
