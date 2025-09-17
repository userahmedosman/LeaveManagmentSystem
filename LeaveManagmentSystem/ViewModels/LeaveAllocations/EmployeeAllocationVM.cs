using LeaveManagmentSystem.Models;
using System.ComponentModel.DataAnnotations;

namespace LeaveManagmentSystem.ViewModels.LeaveAllocations
{
    public sealed record EmployeeAllocationVM:Base
    {
      
        [Display(Name = "Birth Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateOnly BirthDate { get; init; }
        public bool IsCompletedAllocation { get; init; }
        public List<LeaveAllocationVM> LeaveAllocations { get; init; }
    }
}
