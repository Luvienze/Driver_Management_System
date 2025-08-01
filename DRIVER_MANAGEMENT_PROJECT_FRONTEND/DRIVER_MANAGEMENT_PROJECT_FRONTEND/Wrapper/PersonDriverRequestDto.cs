using DRIVER_MANAGEMENT_PROJECT_FRONTEND.Dto;
using System.Text.Json.Serialization;

namespace DRIVER_MANAGEMENT_PROJECT_FRONTEND.Wrapper
{
    public class PersonDriverRequestDto
    {
        [JsonPropertyName("personDto")]
        public PersonDto PersonDto { get; set; }
        [JsonPropertyName("driverDto")]
        public DriverDto DriverDto { get; set; }
    }
}
