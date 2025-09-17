using LeaveManagmentSystem.Models;
using System.ComponentModel.DataAnnotations;

namespace LeaveManagmentSystem.ViewModels.LeaveTypes
{
    public sealed record LeaveTypeEditVM:BaseViewModel
    {
        [Required]
        [Length(4, 15, ErrorMessage = "Leave type name should be more than 4 chars and less than 15 char")]
        public string Name { get; init; } = string.Empty;
        [Required]
        [Range(1, 90, ErrorMessage = "Leave duration should be between 1 and 90 days")]
        [Display(Name = "Allocation Days")]
        public int LeaveDurationInDays { get; init; }
    }
}
