using BoardService.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Repository.Abstractions;

namespace Repository.Implementation
{
    public class BoardColumnRepository(DbContext context) : Repository<BoardColumn, Guid>(context), IBoardColumnRepository
    {
    }
}
