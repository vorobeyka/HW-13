using System.Text.Json.Serialization;

namespace DepsTestApp.Models
{
    internal class Settings
    {
        [JsonPropertyName("address")]
        public string Address { get; set; }
    }
}
