using BoardService.Domain.Abstraction;
using BoardService.Domain.Entity;

namespace Repository.Abstractions
{
    public interface ITaskItemRepository : IRepository<TaskItem, Guid>
    {
        public Task<List<TaskItem>> GetFilteredAsync(Guid boardColumnId, IPageFilter filter);
    }
}
