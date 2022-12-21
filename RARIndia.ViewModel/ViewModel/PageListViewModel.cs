namespace RARIndia.ViewModel
{
    public class PageListViewModel
    {
        public int Page { get; set; }
        public short RecordPerPage { get; set; }
        public int TotalResults { get; set; }
        public int TotalPages { get; set; }
        public string SearchBy { get; set; } = string.Empty;
    }
}