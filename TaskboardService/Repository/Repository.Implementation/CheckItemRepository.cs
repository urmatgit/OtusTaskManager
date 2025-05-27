using BoardService.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Repository.Abstractions;

namespace Repository.Implementation
{
    public class CheckItemRepository(DbContext context) : Repository<CheckItem, Guid>(context), ICheckItemRepository
    {
    }
}
