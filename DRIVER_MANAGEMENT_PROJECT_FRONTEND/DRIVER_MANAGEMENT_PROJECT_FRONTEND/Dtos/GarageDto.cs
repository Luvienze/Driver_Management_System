using System.Text.Json;
using System.Text.Json.Serialization;

namespace DRIVER_MANAGEMENT_PROJECT_FRONTEND.Dto
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
