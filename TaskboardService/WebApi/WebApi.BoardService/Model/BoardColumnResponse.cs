using Services.Contract.BorderColumn;

namespace WebApi.BoardService.Model
{
    public record class BoardColumnResponse : FilteredResponse<List<BoardColumnDto>>
    {
    }
}
