using System.Text.Json.Serialization;

namespace DepsTestApp.Models
{
    internal class Account
    {
        [JsonPropertyName("login")]
        public string Login { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}
