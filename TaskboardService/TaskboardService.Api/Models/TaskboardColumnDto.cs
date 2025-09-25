using System.Text.Json.Serialization;

namespace TaskboardService.Api.Models
{
    public class TaskboardColumnDto
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("taskboardId")]
        public Guid TaskboardId { get; set; }
        [JsonPropertyName("sortOrder")]
        public float SortOrder { get; set; }
        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;
        [JsonPropertyName("headerColor")]
        public string Color { get; set; } = string.Empty;
        [JsonPropertyName("wipLimit")]
        public int WipLimit { get; set; }
        [JsonPropertyName("tasks")]
        public List<TaskItemDto> Tasks { get; set; } = [];
    }
}