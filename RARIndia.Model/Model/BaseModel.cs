namespace RARIndia.Model
{
    public class BaseModel
    {
        public bool HasError { get; set; } = false;
        public string ErrorMessage { get; set; }
        public int ErrorCode { get; set; }
    }
}
