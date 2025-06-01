using AutoMapper;
using BoardService.Domain.Entity;
using Repository.Abstractions;
using Services.Abstractions;
using Services.Contract.TaskBoard;

namespace Services.Implementation
{
    /// <summary>
    /// Сервис доски задач
    /// </summary>
    /// <param name="mapper"></param>
    /// <param name="taskBoardRepository"></param>
    public class TaskBoardService(
        IMapper mapper,
        ITaskBoardRepository taskBoardRepository
        ) : ITaskBoardService
    {
        /// <summary>
        /// Получить доску задач
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>TaskBoardDto</returns>        
        public async Task<TaskBoardDto?> GetByIdAsync(Guid id)
        {
            var taskBoard = await taskBoardRepository.GetAsync(id, CancellationToken.None);
            return taskBoard == null ? null : mapper.Map<TaskBoard, TaskBoardDto>(taskBoard);
        }
    }
}
