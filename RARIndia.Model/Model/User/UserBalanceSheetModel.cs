namespace RARIndia.Model
{
    public class UserBalanceSheetModel : BaseModel
    {
        public short BalsheetID { get; set; }
        public string ActBalsheetHeadDesc { get; set; }
        public string CentreCode { get; set; }
        public string CentreName { get; set; }
        public string HoCoRoScFlag { get; set; }
    }
}
