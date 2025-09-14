using BoardService.Domain.Model;

namespace WebApi.BoardService.Model
{
    /// <summary>
    /// Filtered Response
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public record class FilteredResponse<T> : SimpleResponse<T>
    {   
        /// <summary>
        /// Filter
        /// </summary>
        public required PageFilter Filter { get; set; }
    }
}
