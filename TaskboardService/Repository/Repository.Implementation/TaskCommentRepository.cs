using BoardService.Domain.Entity;
using Infrastructure.EntityFramework;
using Repository.Abstractions;

namespace Repository.Implementation
{
    public class TaskCommentRepository(DatabaseContext context) : Repository<TaskComment, Guid>(context), ITaskCommentRepository
    {
    }
}
