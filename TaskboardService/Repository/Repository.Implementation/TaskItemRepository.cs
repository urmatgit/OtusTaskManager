using BoardService.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Repository.Implementation;

namespace Repository.Abstractions
{
    public class TaskItemRepository(DbContext context) : Repository<TaskItem, Guid>(context), ITaskItemRepository
    {
    }
}
