using System.Text.Json.Serialization;

namespace Entities.DataTransferObjects
{
    public class RoleDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("roleName")]
        public int RoleName { get; set; }

        [JsonPropertyName("personId")]
        public int PersonId { get; set; }
    }
}
