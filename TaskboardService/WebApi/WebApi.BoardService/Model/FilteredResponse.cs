using BoardService.Domain.Model;

namespace WebApi.BoardService.Model
{
    public record class FilteredResponse<T>
    {
        public T? Data { get; set; }
        public required PageFilter Filter { get; set; }
    }
}
