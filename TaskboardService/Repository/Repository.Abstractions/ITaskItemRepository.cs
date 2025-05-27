using BoardService.Domain.Entity;

namespace Repository.Abstractions
{
    public interface ITaskItemRepository : IRepository<TaskItem, Guid>
    {
    }
}
