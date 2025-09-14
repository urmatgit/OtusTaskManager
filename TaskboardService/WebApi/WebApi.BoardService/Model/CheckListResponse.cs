using Services.Contract.CheckList;

namespace WebApi.BoardService.Model
{
    /// <summary>
    /// CheclList response
    /// </summary>
    public record class CheckListResponse : SimpleResponse<List<CheckListDto>>
    {
    }
}
