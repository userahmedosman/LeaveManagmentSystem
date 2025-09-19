using LeaveManagmentSystem.Models;

namespace LeaveManagmentSystem.ViewModels.Periods
{
    public sealed record PeriodReadVM:BaseViewModel
    {
        public string Name { get; init; } = string.Empty;
        public DateOnly StartDate { get; init; }
        public DateOnly EndDate { get; init; }
    }
}
