using Newtonsoft.Json;

using TaskboardService.DataAccess.Enums;

namespace TaskboardService.Api.Models
{
    public class TaskboardDto
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }
        [JsonProperty("projectId")]
        public Guid ProjectId { get; set; }
        [JsonProperty("sortOrder")]
        public float SortOrder { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; } = string.Empty;
        [JsonProperty("status")]
        public TaskboardStatus Status { get; set; }
        [JsonProperty("lastOpened")]
        public bool LastOpened { get; set; }
        [JsonProperty("columns")]
        public List<TaskboardColumnDto> Columns { get; set; } = [];
    }
}