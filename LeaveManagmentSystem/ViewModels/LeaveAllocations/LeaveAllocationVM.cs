using LeaveManagmentSystem.Models;
using LeaveManagmentSystem.ViewModels.LeaveTypes;
using LeaveManagmentSystem.ViewModels.Periods;

namespace LeaveManagmentSystem.ViewModels.LeaveAllocations
{
    public sealed record LeaveAllocationVM:BaseViewModel
    {
        [Display(Name = "Number Of Days")]
        public int Days { get; init; }

        [Display(Name = "Allocation Period")]
        public PeriodReadVM Period { get; init; } = new PeriodReadVM();

        [Display(Name = "Leave Type")]
        public LeaveTypeReadOnlyVM LeaveType { get; init; }= new LeaveTypeReadOnlyVM();
    }
}
