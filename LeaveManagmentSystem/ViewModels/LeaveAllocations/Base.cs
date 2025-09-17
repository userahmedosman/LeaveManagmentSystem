namespace LeaveManagmentSystem.ViewModels.LeaveAllocations
{
    public record Base
    {
        public string Id { get; init; } = string.Empty;

        [Display(Name = "First Name")]
        public string FirstName { get; init; } = string.Empty ;

        [Display(Name = "Last Name")]
        public string LastName { get; init; } = string.Empty;

        [Display(Name = "Email")]
        public string Email { get; init; } = string.Empty;
    }
}
