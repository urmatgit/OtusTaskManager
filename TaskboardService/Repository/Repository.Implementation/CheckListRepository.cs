using BoardService.Domain.Entity;
using Infrastructure.EntityFramework;
using Repository.Abstractions;

namespace Repository.Implementation
{
    public class CheckListRepository(DatabaseContext context) : Repository<CheckList, Guid>(context), ICheckListRepository
    {
    }
}
