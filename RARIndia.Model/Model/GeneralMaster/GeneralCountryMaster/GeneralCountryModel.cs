namespace RARIndia.Model
{
    public class GeneralCountryModel : BaseModel
    {
        public short CountryId { get; set; }
        public string CountryName { get; set; }
        public string CountryCode { get; set; }
        public bool DefaultFlag { get; set; }
        public short SeqNo { get; set; }
    }
}
