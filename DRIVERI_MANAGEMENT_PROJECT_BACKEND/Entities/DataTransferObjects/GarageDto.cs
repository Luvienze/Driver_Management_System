using System.Text.Json.Serialization;

namespace Entities.DataTransferObjects
{
    public class GarageDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("garageName")]
        public string GarageName { get; set; }

        [JsonPropertyName("garageAddress")]
        public string GarageAddress { get; set; }
    }
}
