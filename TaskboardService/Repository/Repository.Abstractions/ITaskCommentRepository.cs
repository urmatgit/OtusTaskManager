using BoardService.Domain.Abstraction;
using BoardService.Domain.Entity;

namespace Repository.Abstractions
{
    public interface ITaskCommentRepository : IRepository<TaskComment, Guid>
    {
        public Task<List<TaskComment>> GetFilteredAsync(Guid taskItemId, IPageFilter filter);
    }
}
