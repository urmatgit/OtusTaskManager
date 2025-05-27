using BoardService.Domain.Entity;

namespace Repository.Abstractions
{
    public interface ITaskCommentRepository : IRepository<TaskComment, Guid>
    {
    }
}
