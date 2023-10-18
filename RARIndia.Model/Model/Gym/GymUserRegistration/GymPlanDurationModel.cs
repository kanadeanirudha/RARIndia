namespace RARIndia.Model
{
    public class GymPlanDurationModel : BaseModel
    {
        public short GymPlanDurationMasterId { get; set; }
        public string PlanDuration { get; set; }
        public bool IsActive { get; set; }
    }
}
