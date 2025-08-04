
using Entities.DataTransferObjects;
using System.Text.Json.Serialization;

namespace Entities.Wrapper
{
    public class PersonDriverRequestDto
    {
        [JsonPropertyName("personDto")]
        public PersonDto PersonDto{ get; set; }

        [JsonPropertyName("driverDto")]
        public DriverDto DriverDto { get; set; }
    }
}
