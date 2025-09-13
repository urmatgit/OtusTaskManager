using BoardService.Domain.Entity;
using Infrastructure.EntityFramework;
using Repository.Abstractions;

namespace Repository.Implementation
{
    public class TaskBoardRepository(DatabaseContext context) : Repository<TaskBoard, Guid>(context), ITaskBoardRepository
    {
    }
}
