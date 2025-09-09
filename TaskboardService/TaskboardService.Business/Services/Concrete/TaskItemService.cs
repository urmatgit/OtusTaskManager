using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using MongoDB.Driver;

using TaskboardService.Business.Services.Abstract;
using TaskboardService.Business.Settings;
using TaskboardService.DataAccess.Entity;

namespace TaskboardService.Business.Services.Concrete
{
    public class TaskItemService(IMongoClient mongoClient, IOptions<WebAppSettings> options, ILogger<TaskItemService> logger) : ITaskItemService
    {
        private readonly IMongoCollection<TaskItem> _taskItems = mongoClient.GetDatabase(options.Value.DbName).GetCollection<TaskItem>("taskItems");
        private readonly ILogger<TaskItemService> _logger = logger;

        public async Task<TaskItem> CreateTaskItemAsync(TaskItem taskItem)
        {
            taskItem.CreatedDate = DateTime.UtcNow;
            taskItem.UpdatedDate = DateTime.UtcNow;

            await _taskItems.InsertOneAsync(taskItem);
            return taskItem;
        }

        public async Task<bool> DeleteTaskItemAsync(Guid id)
        {
            var result = await _taskItems.DeleteOneAsync(t => t.Id == id);
            return result.DeletedCount > 0;
        }

        public async Task<TaskItem> GetTaskItemAsync(Guid taskItemId)
        {
            var result = await _taskItems.FindAsync(t=>t.Id == taskItemId);
            return result.FirstOrDefault();
        }

        public async Task<List<TaskItem>> GetTaskItemColumnList(Guid taskboardId, Guid taskboardColumnId)
        {
            var result = await _taskItems.FindAsync(t => t.TaskboardId == taskboardId && t.TaskboardColumnId == taskboardColumnId);
            return result.ToList();
        }

        public async Task<List<TaskItem>> GetTaskItemListAsync(Guid taskboardId)
        {
            var result = await _taskItems.FindAsync(t => t.TaskboardId == taskboardId);
            return result.ToList();
        }

        public async Task<TaskItem?> UpdateTaskItemAsync(Guid taskItemId, TaskItem taskItem)
        {
            taskItem.UpdatedDate = DateTime.UtcNow;
            var result = await _taskItems.ReplaceOneAsync(t => t.Id == taskItemId, taskItem);
            return result.MatchedCount > 0 ? taskItem : null;
        }
    }
}