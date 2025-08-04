using System.Text.Json.Serialization;

namespace Entities.DataTransferObjects
{
    public class PersonDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("firstName")]
        public string FirstName { get; set; }

        [JsonPropertyName("lastName")]
        public string LastName { get; set; }

        [JsonPropertyName("gender")]
        public int Gender { get; set; }

        [JsonPropertyName("bloodGroup")]
        public int BloodGroup { get; set; }

        [JsonPropertyName("DateOfBirth")]
        public DateOnly DateOfBirth{ get; set; }

        [JsonPropertyName("phone")]
        public string Phone { get; set; }

        [JsonPropertyName("address")]
        public string Address { get; set; }

        [JsonPropertyName("imageUrl")]
        public string ImageUrl { get; set; }

        [JsonPropertyName("registrationNumber")]
        public string RegistrationNumber { get; set; }

        [JsonPropertyName("dateOfStart")]
        public DateOnly DateOfStart { get; set; }

        [JsonPropertyName("isDeleted")]
        public bool IsDeleted { get; set; }
    }
}
