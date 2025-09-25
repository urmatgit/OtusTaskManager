using System.Text.Json.Serialization;

using TaskboardService.DataAccess.Enums;

namespace TaskboardService.Api.Models
{
    public class TaskItemDto
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("taskboardId")]
        public Guid TaskboardId { get; set; }

        [JsonPropertyName("taskboardColumnId")]
        public Guid TaskboardColumnId { get; set; }

        [JsonPropertyName("sortOrder")]
        public float SortOrder { get; set; }

        [JsonPropertyName("title")]
        public required string Title { get; set; }

        [JsonPropertyName("description")]
        public required string Description { get; set; }

        [JsonPropertyName("executionDate")]
        public required DateTime ExecutionDate { get; set; }

        [JsonPropertyName("factExecutionDate")]
        public DateTime? FactExecutionDate { get; set; }

        [JsonPropertyName("priority")]
        public TaskPriority Priority { get; set; }
    }
}