﻿namespace RARIndia.ViewModel
{
    public abstract class BaseViewModel
    {
        public int CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedDate { get; set; }

        public bool HasError { get; set; }
        public string ErrorMessage { get; set; }

    }
}