using BoardService.Domain.Abstraction;
using BoardService.Domain.Entity;

namespace Repository.Abstractions
{
    public interface IBoardColumnRepository : IRepository<BoardColumn, Guid>
    {
        public Task<List<BoardColumn>> GetFilteredAsync(Guid taskBoardId, IPageFilter filter);
    }
}
