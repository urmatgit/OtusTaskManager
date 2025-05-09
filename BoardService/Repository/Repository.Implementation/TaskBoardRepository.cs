using BoardService.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Repository.Abstractions;

namespace Repository.Implementation
{
    class TaskBoardRepository : Repository<TaskBoard, Guid>, ITaskBoardRepository
    {
        public TaskBoardRepository(DbContext context) : base(context)
        {
        }
    }
}
