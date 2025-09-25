using System.Text.Json.Serialization;

using TaskboardService.DataAccess.Enums;

namespace TaskboardService.Api.Models
{
    public class TaskboardDto
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("projectId")]
        public Guid ProjectId { get; set; }
        [JsonPropertyName("sortOrder")]
        public float SortOrder { get; set; }
        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;
        [JsonPropertyName("status")]
        public TaskboardStatus Status { get; set; }
        [JsonPropertyName("lastOpened")]
        public bool LastOpened { get; set; }
        [JsonPropertyName("columns")]
        public List<TaskboardColumnDto> Columns { get; set; } = [];
    }
}