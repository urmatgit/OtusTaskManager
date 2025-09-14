namespace WebApi.BoardService.Model
{
    /// <summary>
    /// Simple Http response
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public record class SimpleResponse<T>
    {
        /// <summary>
        /// payload
        /// </summary>
        public T? Data { get; set; }
    }
}
