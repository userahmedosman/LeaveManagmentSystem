using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeaveManagmentSystem.Configrations
{
    public class ApplicationUserConfig : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            var hasher = new PasswordHasher<ApplicationUser>();
            builder.HasData(
                new ApplicationUser
                {
                    Id = "3C0BDB6C-3412-4E27-97AE-909F54967281",
                    FirstName = "Ahmed",
                    LastName = "Osman",
                    BirthDate = new DateOnly(1998, 12, 19),
                    Email = "ahmedhaj000@gmail.com",
                    NormalizedEmail = "AHMEDHAJ000@GMAIL.COM",
                    NormalizedUserName = "AHMEDHAJ000@GMAIL.COM",
                    UserName = "ahmedhaj000@gmail.com",
                    PasswordHash = hasher.HashPassword(null, "Hajo1!"),
                    EmailConfirmed = true,
                    isDeleted = false,
                    DeletedAt = null
                });
        }
    }
}
