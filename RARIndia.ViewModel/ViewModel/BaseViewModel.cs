namespace RARIndia.ViewModels
{
    public abstract class BaseViewModel
    {
        public int CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedDate { get; set; }

        public int Page { get; set; }
        public int RecordPerPage { get; set; }
        public int TotalResults { get; set; }
        public int TotalPages { get; set; }
        public string SearchBy { get; set; } = string.Empty;

    }
}