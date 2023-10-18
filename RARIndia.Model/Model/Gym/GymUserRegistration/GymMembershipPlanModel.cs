namespace RARIndia.Model
{
    public class GymMembershipPlanModel : BaseModel
    {
        public short GymMembershipPlanMasterId { get; set; }
        public string MembershipPlanName { get; set; }
        public bool IsActive { get; set; }
    }
}
