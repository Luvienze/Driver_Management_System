using System.Text.Json.Serialization;

namespace Entities.DataTransferObjects
{
    public class DriverDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("personFirstName")]
        public string PersonFirstName { get; set; }

        [JsonPropertyName("personLastName")]
        public string PersonLastName { get; set; }

        [JsonPropertyName("phoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonPropertyName("registrationNumber")]
        public string RegistrationNumber { get; set; }

        [JsonPropertyName("garage")]
        public string Garage { get; set; }

        [JsonPropertyName("chiefId")]
        public int ChiefId { get; set; }

        [JsonPropertyName("chiefFirstName")]
        public string ChiefFirstName{ get; set; }

        [JsonPropertyName("chiefLastName")]
        public string ChiefLastName { get; set; }

        [JsonPropertyName("cadre")]
        public int Cadre { get; set; }

        [JsonPropertyName("dayOff")]
        public int DayOff { get; set; }

        [JsonPropertyName("isActive")]
        public bool IsActive { get; set; }
    }
}
