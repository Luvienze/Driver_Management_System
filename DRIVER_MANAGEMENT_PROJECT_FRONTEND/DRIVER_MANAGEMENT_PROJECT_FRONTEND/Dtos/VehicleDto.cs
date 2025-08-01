using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace DRIVER_MANAGEMENT_PROJECT_FRONTEND.Dtos
{
    public class VehicleDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("doorNo")]
        public string DoorNo { get; set; }

        [JsonPropertyName("capacity")]
        public int Capacity { get; set; }

        [JsonPropertyName("fuelTank")]
        public int FuelTank { get; set; }

        [JsonPropertyName("plate")]
        public string Plate { get; set; }

        [JsonPropertyName("personOnFoot")]
        public int PersonOnFoot { get; set; }

        [JsonPropertyName("personOnSit")]
        public int PersonOnSit { get; set; }

        [JsonPropertyName("suitableForDisabled")]
        public bool SuitableForDisabled { get; set; }

        [JsonPropertyName("modelYear")]
        public int ModelYear { get; set; }

        [JsonPropertyName("garageId")]
        public int GarageId { get; set; }

        [JsonPropertyName("garageName")]
        public string GarageName { get; set; }

        [JsonPropertyName("status")]
        public int Status { get; set; }
    }
}
