namespace RARIndia.Model
{
    public class GeneralDepartmentModel : BaseModel
    {
        public short DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string DeptShortCode { get; set; }
        public string PrintShortDesc { get; set; }
        public bool WorkActivity { get; set; }
    }
}
