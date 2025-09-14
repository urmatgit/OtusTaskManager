using BoardService.Domain.Abstraction;
using BoardService.Domain.Entity;
using Infrastructure.EntityFramework;
using Repository.Implementation;
using Repository.Implementation.Extension;

namespace Repository.Abstractions
{
    public class TaskItemRepository(DatabaseContext context) : Repository<TaskItem, Guid>(context), ITaskItemRepository
    {
        public async Task<List<TaskItem>> GetFilteredAsync(Guid boardColumnId, IPageFilter filter)
        {
            var query = GetAll(true)
                .Where(x => x.BoardColumnId == boardColumnId)
                .OrderBy(x => x.CreateDate)
                .AddFilter(filter);

            return await GetQueryAsync(query);
        }
    }
}
