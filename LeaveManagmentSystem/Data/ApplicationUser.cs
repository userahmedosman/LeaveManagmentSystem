using Microsoft.AspNetCore.Identity;

namespace LeaveManagmentSystem.Data
{
    public class ApplicationUser:IdentityUser
    {
        public string FirstName {  get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateOnly BirthDate { get; set; }
        public bool isDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }
    }
}
