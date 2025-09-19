using LeaveManagmentSystem.Services.LeaveRequests;
using System.ComponentModel;

namespace LeaveManagmentSystem.ViewModels.LeaveReqests
{
    public sealed record LeaveRequestAdminDetailVM
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

        [DisplayName("Request Comment")]
        public string? RequestComment { get; set; }

        [DisplayName("Is Emergency Request")]
        public bool? IsEmergency { get; set; } = false;

        [DisplayName("Employee Info")]
        public ApplicationUser? Employee { get; set; }

        [DisplayName("Reviewed By")]
        public ApplicationUser? Reviewer { get; set; }

        [DisplayName("Review Comment")]
        public string? ReviewComment { get; set; }
    }
}
