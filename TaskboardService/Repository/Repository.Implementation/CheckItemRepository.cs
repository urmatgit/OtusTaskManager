using BoardService.Domain.Entity;
using Infrastructure.EntityFramework;
using Repository.Abstractions;

namespace Repository.Implementation
{
    public class CheckItemRepository(DatabaseContext context) : Repository<CheckItem, Guid>(context), ICheckItemRepository
    {
    }
}
