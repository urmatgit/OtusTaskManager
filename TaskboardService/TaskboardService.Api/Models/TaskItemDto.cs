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
        public string Title { get; set; } = string.Empty;

        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;

        [JsonPropertyName("executionDate")]
        public DateTime? ExecutionDate { get; set; }

        [JsonPropertyName("factExecutionDate")]
        public DateTime? FactExecutionDate { get; set; }

        [JsonPropertyName("priority")]
        public TaskPriority Priority { get; set; }
        [JsonPropertyName("progress")]
        public int Progress {  get; set; }
        [JsonPropertyName("headerColor")]
        public string HeaderColor { get; set; } = string.Empty;
        [JsonPropertyName("comments")]
        public List<TaskCommentDto> Comments { get; set; } = [];
        [JsonPropertyName("attachments")]
        public List<TaskAttachmentDto> Attachments { get; set; } = [];
    }
}