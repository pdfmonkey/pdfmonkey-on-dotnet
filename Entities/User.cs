namespace Entities
{
    using Newtonsoft.Json;

    public class User : EntityBase
    {
        public new static string Identifier => "current_user";

        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("auth_token")]
        public string? AuthToken { get; set; }

        [JsonProperty("available_documents")]
        public int AvailableDocuments { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("current_plan")]
        public string? CurrentPlan { get; set; }

        [JsonProperty("current_plan_interval")]
        public string? CurrentPlanInterval { get; set; }

        [JsonProperty("desired_name")]
        public string? DesiredName { get; set; }

        [JsonProperty("email")]
        public string? Email { get; set; }

        [JsonProperty("lang")]
        public string? Lang { get; set; }

        [JsonProperty("paying_customer")]
        public bool PayingCustomer { get; set; }

        [JsonProperty("share_links")]
        public bool ShareLinks { get; set; }

        [JsonProperty("trial_ends_on")]
        public DateTime TrialEndsOn { get; set; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("block_resources")]
        public bool BlockResources { get; set; }
    }
}