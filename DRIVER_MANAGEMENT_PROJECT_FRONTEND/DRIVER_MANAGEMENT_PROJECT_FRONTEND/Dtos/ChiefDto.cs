using System.Text.Json.Serialization;

namespace DRIVER_MANAGEMENT_PROJECT_FRONTEND.Dto
{
    public class ChiefDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("personId")]
        public int PersonId { get; set; }

        [JsonPropertyName("chiefFirstName")]
        public string ChiefFirstName { get; set; }

        [JsonPropertyName("chiefLastName")]
        public string ChiefLastName { get; set; }

        [JsonPropertyName("garageName")]
        public string GarageName { get; set; }

        [JsonPropertyName("garageId")]
        public int? GarageId { get; set; }

        [JsonPropertyName("chieftiencyName")]
        public string? ChieftiencyName { get; set; }

        [JsonPropertyName("active")]
        public bool Active { get; set; }
    }

}
