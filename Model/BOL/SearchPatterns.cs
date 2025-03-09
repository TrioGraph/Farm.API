namespace Farm.Models
{
    public class SearchPatterns
    {
        public string? SearchText { get; set; }
        public int? PageIndex { get; set; }
        public int? PageSize { get; set; }
        public string? SortColumn { get; set; }
        public string? SortDirection { get; set; }
        
    }
}