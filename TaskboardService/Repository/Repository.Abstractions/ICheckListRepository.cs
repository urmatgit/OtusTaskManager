using BoardService.Domain.Entity;

namespace Repository.Abstractions
{
    public interface ICheckListRepository : IRepository<CheckList, Guid>
    {
        public Task<List<CheckList>> GetChecksAsync(Guid taskItemId);
    }
}
