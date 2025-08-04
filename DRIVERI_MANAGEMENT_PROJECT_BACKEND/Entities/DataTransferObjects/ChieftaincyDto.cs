using System.Text.Json.Serialization;
namespace Entities.DataTransferObjects
{
    public class ChieftaincyDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("chieftiencyName")]
        public string ChieftiencyName { get; set; }

        [JsonPropertyName("garageId")]
        public int GarageId { get; set; }

        [JsonPropertyName("garageName")]
        public string GarageName { get; set; }
    }
}
