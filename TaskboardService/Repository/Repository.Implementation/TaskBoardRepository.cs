using BoardService.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Repository.Abstractions;

namespace Repository.Implementation
{
    public class TaskBoardRepository(DbContext context) : Repository<TaskBoard, Guid>(context), ITaskBoardRepository
    {
    }
}
