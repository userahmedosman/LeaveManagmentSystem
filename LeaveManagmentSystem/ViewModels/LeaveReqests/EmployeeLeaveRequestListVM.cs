using LeaveManagmentSystem.Models;
using LeaveManagmentSystem.Services.LeaveRequests;
using System.ComponentModel;

namespace LeaveManagmentSystem.ViewModels.LeaveReqests
{
    public sealed record EmployeeLeaveRequestListVM:BaseViewModel
    {
        [DisplayName("Total Leave Requests")]

        public int TotalRequests { get; set; }

        [DisplayName("Approved Requests")]

        public int ApprovedRequests { get; set; }

        [DisplayName("Pending Requests")]

        public int PendingRequests { get; set; }

        [DisplayName("Rejected Requests")]

        public int DeclinedRequests { get; set; }

        [DisplayName("Canceled Requests")]

        public int CancelledRequests { get; set; }


        public List<LeaveRequestReadOnlyVM> LeaveRequests { get; set; } = [];
    }
}
