using BoardService.Domain.Abstraction;
using BoardService.Domain.Entity;

namespace Repository.Abstractions
{
    public interface ITaskBoardRepository : IRepository<TaskBoard, Guid>
    {
        public Task<List<TaskBoard>> GetFilteredAsync(IPageFilter filter);
    }
}
