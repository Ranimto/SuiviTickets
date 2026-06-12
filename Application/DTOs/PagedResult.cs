namespace Application.Common.Models
{
    /// <summary>
    /// Résultat paginé générique.
    /// </summary>
    public class PagedResult<T>
    {
        public List<T> Items { get; set; } = new();

        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public int TotalCount { get; set; }
    }
}