using BoardService.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Repository.Abstractions;

namespace Repository.Implementation
{
    public class CheckListRepository(DbContext context) : Repository<CheckList, Guid>(context), ICheckListRepository
    {
    }
}
