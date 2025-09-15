
namespace LeaveManagmentSystem.CustomAttribute
{
    public class BirthDateRange : ValidationAttribute
    {
        private const int StartingYear = 1960;
        private const int MinAge = 18;

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is not DateOnly birthDate)
            {
                return new ValidationResult("Invalid date format.");
            }

            var today = DateOnly.FromDateTime(DateTime.Today);

            // Dynamically calculate min birthdate (e.g., Jan 1, 1960 + (current year - 1960))
            var yearsSinceStart = today.Year - StartingYear;
            var minBirthDate = new DateOnly((today.Year - yearsSinceStart), 1, 1);

            // Max birthdate is 18 years ago from today
            var maxBirthDate = today.AddYears(-MinAge);

            if (birthDate < minBirthDate || birthDate > maxBirthDate)
            {
                return new ValidationResult($"Birthdate must be between {minBirthDate:yyyy-MM-dd} and {maxBirthDate:yyyy-MM-dd}");
            }

            return ValidationResult.Success;
        }
    }
}
