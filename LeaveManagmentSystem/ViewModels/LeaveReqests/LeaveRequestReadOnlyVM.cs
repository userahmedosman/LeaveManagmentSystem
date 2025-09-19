using LeaveManagmentSystem.Models;
using System.ComponentModel;
using LeaveManagmentSystem.Services.LeaveRequests;

namespace LeaveManagmentSystem.ViewModels.LeaveReqests
{
    public sealed record LeaveRequestReadOnlyVM:BaseViewModel
    {
        [DisplayName("Start Date")]
        public DateOnly StartDate { get; set; }

        [DisplayName("End Date")]
        public DateOnly EndDate { get; set; }

        [DisplayName("Total Days")]
        public int NumberOfDays { get; set; }

        [DisplayName("Leave Reason")]
        public string LeaveType { get; set; } = string.Empty;

        [DisplayName("Status")]
        public LeaveRequestStatusEnum LeaveRequestStatusEnum { get; set; }


        public string EmployeeId { get; set; } = default!;
    }
}
