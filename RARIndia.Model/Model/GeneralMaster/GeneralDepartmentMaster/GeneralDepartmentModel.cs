namespace RARIndia.Model
{
    public class GeneralDepartmentModel : BaseModel
    {
        public short ID { get; set; }
        public string DepartmentName { get; set; }
        public string DeptShortCode { get; set; }
        public string PrintShortDesc { get; set; }
        public bool WorkActivity { get; set; }
    }
}
