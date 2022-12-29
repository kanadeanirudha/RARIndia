namespace RARIndia.Model
{
    public class UserMenuModel : BaseModel
    {
        public short ID { get; set; }
        public short ModuleID { get; set; }
        public string ModuleCode { get; set; }
        public string MenuCode { get; set; }
        public string MenuName { get; set; }
        public short? ParentMenuID { get; set; }
        public int? MenuDisplaySeqNo { get; set; }
        public string MenuLink { get; set; }
        public string MenuToolTip { get; set; }
        public string MenuIconName { get; set; }
    }
}
