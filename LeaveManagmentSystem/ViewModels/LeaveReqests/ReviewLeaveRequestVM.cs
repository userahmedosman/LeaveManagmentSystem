using LeaveManagmentSystem.Models;

namespace LeaveManagmentSystem.ViewModels.LeaveReqests
{
    public sealed record ReviewLeaveRequestVM:BaseViewModel
    {
        public int LeaveRequestStatusId { get; set; }
        public string? ReviewComment { get; set; }
    }
}
