using BoardService.Domain.Entity;
using Infrastructure.EntityFramework;
using Repository.Abstractions;

namespace Repository.Implementation
{
    public class BoardColumnRepository(DatabaseContext context) : Repository<BoardColumn, Guid>(context), IBoardColumnRepository
    {
    }
}
