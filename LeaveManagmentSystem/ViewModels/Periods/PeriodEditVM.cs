using LeaveManagmentSystem.Models;
namespace LeaveManagmentSystem.ViewModels.Periods
{
    public sealed record PeriodEditVM:BaseViewModel
    {
        [Required]
        [Length(4, 15, ErrorMessage = "Period name should be more than or equal to 4 chars and less than 15 char")]
        public string Name { get; init; } = string.Empty;

    }
}
