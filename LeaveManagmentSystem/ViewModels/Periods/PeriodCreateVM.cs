

namespace LeaveManagmentSystem.ViewModels.Periods
{
    public sealed record PeriodCreateVM
    {
        [Length(4, 15, ErrorMessage = "Period name should be more than or equal to 4 chars and less than 15 char")]
        public string Name { get; init; } = string.Empty;

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        public DateOnly StartDate { get; init; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        public DateOnly EndDate { get; init; }
    }
}
