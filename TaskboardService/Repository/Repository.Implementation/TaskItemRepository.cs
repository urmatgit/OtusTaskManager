using BoardService.Domain.Entity;
using Infrastructure.EntityFramework;
using Repository.Implementation;

namespace Repository.Abstractions
{
    public class TaskItemRepository(DatabaseContext context) : Repository<TaskItem, Guid>(context), ITaskItemRepository
    {
    }
}
