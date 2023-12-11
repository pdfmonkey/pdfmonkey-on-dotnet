namespace Entities
{
    using Newtonsoft.Json;

    public class DocumentCard : EntityBase
    {
        public static new string Identifier => "document_card";

        [JsonProperty("id")]
        public Guid? Id { get; set; }

        [JsonProperty("app_id")]
        public Guid? AppId { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("document_template_id")]
        public Guid? DocumentTemplateId { get; set; }

        [JsonProperty("document_template_identifier")]
        public string? DocumentTemplateIdentifier { get; set; }

        [JsonProperty("download_url")]
        public string? DownloadUrl { get; set; }

        [JsonProperty("failure_cause")]
        public object? FailureCause { get; set; }

        [JsonProperty("filename")]
        public string? Filename { get; set; }

        [JsonProperty("meta")]
        public string? Meta { get; set; }

        [JsonProperty("public_share_link")]
        public string? PublicShareLink { get; set; }

        [JsonProperty("status")]
        public string? Status { get; set; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }
}