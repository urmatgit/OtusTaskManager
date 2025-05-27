using BoardService.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Repository.Abstractions;

namespace Repository.Implementation
{
    public class TaskCommentRepository(DbContext context) : Repository<TaskComment, Guid>(context), ITaskCommentRepository
    {
    }
}
