using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using MongoDB.Driver;

using TaskboardService.Business.Services.Abstract;
using TaskboardService.Business.Settings;
using TaskboardService.DataAccess.Entity;

namespace TaskboardService.Business.Services.Concrete
{
    public class TaskboardService(IMongoClient mongoClient, IOptions<WebAppSettings> options, ILogger<TaskboardService> logger) : ITaskboardService
    {
        private readonly IMongoCollection<Taskboard> _taskboards = mongoClient.GetDatabase(options.Value.DbName).GetCollection<Taskboard>("taskboards");
        private readonly ILogger<TaskboardService> _logger = logger;

        public async Task<Taskboard> AddColumnAsync(Guid taskboardId, TaskboardColumn column)
        {
            column.CreatedDate = DateTime.UtcNow;
            column.UpdatedDate = DateTime.UtcNow;

            var builder = Builders<Taskboard>.Update
                .Push(tb => tb.Columns, column);

            var options = new FindOneAndUpdateOptions<Taskboard>
            {
                ReturnDocument = ReturnDocument.After
            };

            return await _taskboards.FindOneAndUpdateAsync(tb => tb.Id == taskboardId, builder, options);            
        }

        public async Task<Taskboard> CreateTaskboardAsync(Taskboard taskboard)
        {
            if (taskboard == null)
            {
                _logger.LogWarning("Передаваемый параметр {taskboard} не может быть null.", nameof(taskboard));
                throw new ArgumentNullException($"Параметр {nameof(taskboard)} не может быть null."); 
            }

            taskboard.CreatedDate = DateTime.UtcNow;
            taskboard.UpdatedDate = DateTime.UtcNow;

            await _taskboards.InsertOneAsync(taskboard);
            return taskboard;
        }

        public async Task<Taskboard> DeleteColumnAsync(Guid taskboardId, Guid columnId)
        {
            var builder = Builders<Taskboard>.Update
                .PullFilter(tb => tb.Columns, col => col.Id == columnId)
                .Set(tb => tb.UpdatedDate, DateTime.UtcNow);

            var options = new FindOneAndUpdateOptions<Taskboard> 
            {
                ReturnDocument = ReturnDocument.After
            };

            return await _taskboards.FindOneAndUpdateAsync(tb => tb.Id == taskboardId, builder, options);
        }

        public async Task<bool> DeleteTaskboardAsync(Guid taskboardId)
        {
            var result = await _taskboards.DeleteOneAsync(tb => tb.Id == taskboardId);
            return result.DeletedCount > 0;
        }

        public async Task<Taskboard> GetTaskboardAsync(Guid taskboardId)
        {
            return await _taskboards.Find(tb => tb.Id == taskboardId).FirstOrDefaultAsync();
        }

        public async Task<List<Taskboard>> GetTaskboardListAsync(Guid projectId)
        {
            return await _taskboards.Find(tb => tb.ProjectId == projectId).SortBy(t => t.SortOrder).ToListAsync();
        }

        public async Task<Taskboard> ReorderColumnAsync(Guid taskboardId, Guid columnId, float newOrder)
        {
            var filter = Builders<Taskboard>.Filter.And(
                Builders<Taskboard>.Filter.Eq(tb => tb.Id, taskboardId),
                Builders<Taskboard>.Filter.ElemMatch(tb => tb.Columns, col => col.Id == columnId)
            );

            var update = Builders<Taskboard>.Update
                .Set("columns.$.sortOrder", newOrder)
                .Set(tb => tb.UpdatedDate, DateTime.UtcNow);

            var options = new FindOneAndUpdateOptions<Taskboard>()
            {
                ReturnDocument = ReturnDocument.After
            };
            return await _taskboards.FindOneAndUpdateAsync(filter, update, options);
        }

        public async Task<Taskboard> UpdateColumnAsync(Guid taskboardId, TaskboardColumn column)
        {
            var filter = Builders<Taskboard>.Filter.And(
                Builders<Taskboard>.Filter.Eq(tb => tb.Id, taskboardId),
                Builders<Taskboard>.Filter.ElemMatch(tb => tb.Columns, col => col.Id == column.Id)
            );

            var update = Builders<Taskboard>.Update
                .Set("columns.$.title", column.Title)
                .Set("columns.$.vipLimit", column.WipLimit)
                .Set(tb => tb.UpdatedDate, DateTime.UtcNow);

            var options = new FindOneAndUpdateOptions<Taskboard>() 
            {
                ReturnDocument = ReturnDocument.After
            };
            return await _taskboards.FindOneAndUpdateAsync(filter, update, options);
        }

        public async Task<Taskboard?> UpdateTaskboardAsync(Taskboard taskboard)
        {
            taskboard.UpdatedDate = DateTime.UtcNow;
            var result = await _taskboards.ReplaceOneAsync(t => t.Id == taskboard.Id, taskboard);
            return result.MatchedCount > 0 ? taskboard : null;
        }
    }
}