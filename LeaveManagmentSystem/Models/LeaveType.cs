using System.ComponentModel.DataAnnotations;

namespace LeaveManagmentSystem.Models
{
    public sealed class LeaveType:BaseEntity
    {
      
        public string Name { get; set; } = string.Empty;
        public int LeaveDurationInDays { get; set; }

        public List<LeaveAllocation>? LeaveAllocations { get; set; }
    }
}
