using RARIndia.ViewModels;

namespace RARIndia.ViewModel
{
    public class GeneralCountryViewModel : BaseViewModel
    {
        public short ID { get; set; }
        public string CountryName { get; set; }
        public string CountryCode { get; set; }
        public bool DefaultFlag { get; set; }
        public short SeqNo { get; set; }
    }
}
