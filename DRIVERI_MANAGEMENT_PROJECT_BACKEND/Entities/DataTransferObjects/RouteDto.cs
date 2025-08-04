using System.Text.Json.Serialization;

namespace Entities.DataTransferObjects
{
    public class RouteDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("routeName")]
        public string RouteName { get; set; }

        [JsonPropertyName("distance")]
        public int Distance { get; set; }

        [JsonPropertyName("duration")]
        public int Duration { get; set; }
    }
}
