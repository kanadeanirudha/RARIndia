namespace RARIndia.Model
{
    public class UserModuleModel : BaseModel
    {
        public short UserModuleMasterId { get; set; }
        public string ModuleCode { get; set; }
        public string ModuleName { get; set; }
        public int? ModuleSeqNumber { get; set; }
        public string ModuleTooltip { get; set; }
        public string ModuleIconName { get; set; }
        public string ModuleColorClass { get; set; }
    }
}
