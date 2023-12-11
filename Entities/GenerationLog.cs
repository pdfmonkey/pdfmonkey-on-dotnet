namespace Entities
{
    using Newtonsoft.Json;

    public class GenerationLog
    {
        [JsonProperty("type")]
        public string? Type { get; set; }

        [JsonProperty("message")]
        public string? Message { get; set; }

        [JsonProperty("timestamp")]
        public string? Timestamp { get; set; }
    }
}