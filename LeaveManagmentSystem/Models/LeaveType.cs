namespace LeaveManagmentSystem.Models
{
    public sealed class LeaveType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int LeaveDurationInDays { get; set; }
    }
}
