using Newtonsoft.Json;

namespace TaskboardService.Api.Models
{
    public class TaskboardColumnDto
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }
        [JsonProperty("sortOrder")]
        public float SortOrder { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; } = string.Empty;
        [JsonProperty("vipLimit")]
        public int VipLimit { get; set; }
    }
}