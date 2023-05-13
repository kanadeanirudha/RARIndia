
using System.Collections.Generic;
using System.Web.Mvc;

namespace RARIndia.ViewModel
{
    public class DropdownViewModel
    {
        public DropdownViewModel()
        {
            DropdownList = new List<SelectListItem>();
        }
        public List<SelectListItem> DropdownList { get; set; }
        public string DropdownName { get; set; }
        public string DropdownType { get; set; }
        public string DropdownSelectedValue { get; set; }
    }
}
