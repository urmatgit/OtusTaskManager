using Services.Contract.BorderColumn;

namespace Services.Abstractions
{
    public interface IBoardColumnService
    {
        Task<BoardColumnDto?> GetByIdAsync(Guid id);
    }
}
