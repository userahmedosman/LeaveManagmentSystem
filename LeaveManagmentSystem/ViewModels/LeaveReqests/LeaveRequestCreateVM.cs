using Microsoft.AspNetCore.Mvc.Rendering;

namespace LeaveManagmentSystem.ViewModels.LeaveReqests
{
    public sealed record LeaveRequestCreateVM:IValidatableObject
    {
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        public DateOnly StartDate { get; init; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        public DateOnly EndDate { get; init; }

        [Required]
        public int LeaveTypeId { get; set; }

        [Required]
        public static string EmployeeId { get; set; } = default!;

        [Display(Name = "Request Comment")]
        [StringLength(250)]
        public string? RequestComment { get; init; }

        [Display(Name = "Priority")]
   
        public bool? IsEmergency { get; set; } = false;


        [Display(Name = "Select Leave Type")]
        public SelectList? LeaveTypes { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (StartDate > EndDate) {
             yield return new ValidationResult("The Start date can't be greater than the End date",[nameof(StartDate), nameof(EndDate)]
             ); 
            }

            if(StartDate < DateOnly.FromDateTime(DateTime.Now))
            {
                yield return new ValidationResult("Start date should be now not yesterday", [nameof(StartDate)]);
            }
        }
    }
}
