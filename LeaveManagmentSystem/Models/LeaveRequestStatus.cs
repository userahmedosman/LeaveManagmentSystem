namespace LeaveManagmentSystem.Models
{
    public sealed class LeaveRequestStatus:BaseEntity
    {
        [StringLength(25)]
        public string Name { get; set; } = string.Empty;
    }
}
