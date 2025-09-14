using BoardService.Domain.Entity;
using Infrastructure.EntityFramework;
using Repository.Abstractions;

namespace Repository.Implementation
{
    public class CheckListRepository(DatabaseContext context) : Repository<CheckList, Guid>(context), ICheckListRepository
    {
        public async Task<List<CheckList>> GetChecksAsync(Guid taskItemId)
        {
            var query = GetAll(true)
                .Where(x => x.TaskId == taskItemId)
                ;

            return await GetQueryAsync(query);
        }
    }
}
