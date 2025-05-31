using BoardService.Domain.Entity;

namespace Repository.Abstractions
{
    public interface ITaskBoardRepository : IRepository<TaskBoard, Guid>
    {
    }
}
