using System.Text.Json.Serialization;

namespace Entities.DataTransferObjects
{
    public class LineDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("lineCode")]
        public string LineCode { get; set; }

        [JsonPropertyName("lineName")]
        public string LineName { get; set; }

        [JsonPropertyName("isActive")]
        public bool IsActive { get; set; }
    }
}
