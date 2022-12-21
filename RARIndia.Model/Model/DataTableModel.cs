namespace RARIndia.Model.Model
{
    public class DataTableModel
    {
        public string SearchBy { get; set; }
        public string SortByColumn { get; set; }
        public string SortBy { get; set; }
        public short PageIndex { get; set; } = 1;
        public short PageSize { get; set; } = 10;
    }
}
