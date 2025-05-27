using AutoMapper;
using BoardService.Domain.Entity;
using Repository.Abstractions;
using Services.Contract.BorderColumn;

namespace Services.Abstractions
{
    public class BoardColumnService(
        IMapper mapper,
        IBoardColumnRepository boardColumnRepository
        ) : IBoardColumnService
    {
        public async Task<BoardColumnDto?> GetByIdAsync(Guid id)
        {
            var course = await boardColumnRepository.GetAsync(id, CancellationToken.None);
            return (course == null) ? null : mapper.Map<BoardColumn, BoardColumnDto>(course);
        }
    }
}
