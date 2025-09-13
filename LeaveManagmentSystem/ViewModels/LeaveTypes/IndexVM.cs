using System.ComponentModel.DataAnnotations;

namespace LeaveManagmentSystem.ViewModels.LeaveTypes
{
    public sealed class IndexVM
    {
   
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        [Display(Name = "Allocation Days")]
        public int LeaveDurationInDays { get; set; }
    }
}
