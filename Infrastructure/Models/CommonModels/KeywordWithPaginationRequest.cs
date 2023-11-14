namespace Infrastructure.Models.CommonModels
{
    public class KeywordWithPaginationRequest : PaginationRequest
    {
        public string Keyword { get; set; } = string.Empty;
    }
}
