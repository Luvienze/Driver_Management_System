using System.Text.Json.Serialization;

namespace Entities.DataTransferObjects
{
    public class TaskDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("registrationNumber")]
        public string RegistrationNumber { get; set; }

        [JsonPropertyName("doorNo")]
        public string DoorNo{ get; set; }

        [JsonPropertyName("routeId")]
        public int RouteId { get; set; }

        [JsonPropertyName("routeName")]
        public string RouteName{ get; set; }

        [JsonPropertyName("lineCode")]
        public string LineCode{ get; set; }

        [JsonPropertyName("direction")]
        public int Direction { get; set; }

        [JsonPropertyName("dateOfStart")]
        public DateTime DateOfStart { get; set; }

        [JsonPropertyName("dateOfEnd")]
        public DateTime DateOfEnd { get; set; }

        [JsonPropertyName("passengerCount")]
        public int PassengerCount { get; set; }

        [JsonPropertyName("orerStart")]
        public DateTime OrerStart{ get; set; }

        [JsonPropertyName("orerEnd")]
        public DateTime OrerEnd { get; set; }

        [JsonPropertyName("chiefStart")]
        public DateTime ChiefStart { get; set; }

        [JsonPropertyName("chiefEnd")]
        public DateTime ChiefEnd { get; set; }

        [JsonPropertyName("status")]
        public int Status { get; set; }
    }
}
