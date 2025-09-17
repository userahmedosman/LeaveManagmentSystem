using LeaveManagmentSystem.Models;
using System.ComponentModel.DataAnnotations;

namespace LeaveManagmentSystem.ViewModels.LeaveTypes
{
    public sealed record LeaveTypeReadOnlyVM:BaseViewModel
    {

        public string Name { get; init; } = string.Empty;

        [Display(Name = "Allocation Days")]
        public int LeaveDurationInDays { get; init; }
    }
}
