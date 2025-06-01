using AutoMapper;
using BoardService.Domain.Entity;
using Repository.Abstractions;
using Services.Abstractions;
using Services.Contract.TaskItem;

namespace Services.Implementation
{
    /// <summary>
    /// Cервис Задача
    /// </summary>
    /// <param name="mapper"></param>
    /// <param name="taskItemRepository"></param>
    public class TaskItemService(
        IMapper mapper,
        ITaskItemRepository taskItemRepository
        ) : ITaskItemService
    {
        /// <summary>
        /// Получить Задачу
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>TaskItemDto</returns>
        public async Task<TaskItemDto?> GetByIdAsync(Guid id)
        {
            var taskItem = await taskItemRepository.GetAsync(id, CancellationToken.None);
            return taskItem == null ? null : mapper.Map<TaskItem, TaskItemDto>(taskItem);
        }
    }
}
