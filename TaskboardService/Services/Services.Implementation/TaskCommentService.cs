using AutoMapper;
using BoardService.Domain.Entity;
using Repository.Abstractions;
using Services.Contract.TaskComment;

namespace Services.Abstractions
{
    /// <summary>
    /// Сервис Комментарий задачи
    /// </summary>
    /// <param name="mapper"></param>
    /// <param name="boardColumnRepository"></param>
    public class TaskCommentService(
        IMapper mapper,
        ITaskCommentRepository taskCommentRepository
        ) : ITaskCommentService
    {
        /// <summary>
        /// Получить Комментарий задачи
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>TaskCommentDto</returns>        
        public async Task<TaskCommentDto?> GetByIdAsync(Guid id)
        {
            var comment = await taskCommentRepository.GetAsync(id, CancellationToken.None);
            return comment == null ? null : mapper.Map<TaskComment, TaskCommentDto>(comment);
        }
    }
}
