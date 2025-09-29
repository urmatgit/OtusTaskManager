using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using MongoDB.Bson;
using MongoDB.Driver;

using TaskboardService.Business.Services.Abstract;
using TaskboardService.Business.Settings;
using TaskboardService.DataAccess.Entity;

namespace TaskboardService.Business.Services.Concrete
{
    public class TaskItemService(IMongoClient mongoClient, IOptions<WebAppSettings> options, ILogger<TaskItemService> logger) : ITaskItemService
    {
        private readonly IMongoCollection<Taskboard> _taskItems = mongoClient.GetDatabase(options.Value.DbName).GetCollection<Taskboard>("taskboards");
        private readonly ILogger<TaskItemService> _logger = logger;

        public async Task<TaskItem> CreateTaskItemAsync(TaskItem taskItem)
        {
            taskItem.CreatedDate = DateTime.UtcNow;
            taskItem.UpdatedDate = DateTime.UtcNow;

            var filter = Builders<Taskboard>.Filter.Eq(tb => tb.Id, taskItem.TaskboardId);

            var update = Builders<Taskboard>.Update
                .Push("columns.$[col].tasks", taskItem)
                .Set(tb => tb.UpdatedDate, DateTime.UtcNow);

            var options = new FindOneAndUpdateOptions<Taskboard> 
            {
                ReturnDocument = ReturnDocument.After,
                ArrayFilters = new ArrayFilterDefinition<Taskboard>[]
                {
                    new BsonDocument("col._id", new BsonBinaryData(taskItem.TaskboardColumnId, GuidRepresentation.Standard))
                }
            };

            var result = await _taskItems.FindOneAndUpdateAsync(filter, update, options);
            if (result == null) 
            {
                throw new ArgumentNullException("Не найден объект для вставки данных");
            }
            return taskItem;
        }

        public async Task<bool> DeleteTaskItemAsync(Guid taskboardId, Guid columnId, Guid id)
        {
            var result = await _taskItems.DeleteOneAsync(t => t.Id == id);
            return result.DeletedCount > 0;
        }

        public async Task<TaskItem> GetTaskItemAsync(Guid taskItemId)
        {
            var result = await _taskItems.FindAsync(t=>t.Id == taskItemId);
            return null;//result.FirstOrDefault();
        }

        public async Task<TaskItem?> UpdateTaskItemAsync(TaskItem taskItem)
        {
            taskItem.UpdatedDate = DateTime.UtcNow;
            //var result = await _taskItems.ReplaceOneAsync(t => t.Id == taskItemId, taskItem);
            return null; //result.MatchedCount > 0 ? taskItem : null;
        }
    }
}