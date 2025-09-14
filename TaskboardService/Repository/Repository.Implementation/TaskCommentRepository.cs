using BoardService.Domain.Abstraction;
using BoardService.Domain.Entity;
using Infrastructure.EntityFramework;
using Repository.Abstractions;
using Repository.Implementation.Extension;

namespace Repository.Implementation
{
    public class TaskCommentRepository(DatabaseContext context) : Repository<TaskComment, Guid>(context), ITaskCommentRepository
    {
        public async Task<List<TaskComment>> GetFilteredAsync(Guid taskItemId, IPageFilter filter)
        {
            var query = GetAll(true)
                .Where(x => x.TaskId == taskItemId)
                .OrderBy(x => x.CreateDate)
                .AddFilter(filter);

            return await GetQueryAsync(query);
        }
    }
}
