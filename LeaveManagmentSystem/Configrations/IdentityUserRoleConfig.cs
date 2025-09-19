using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeaveManagmentSystem.Configrations
{
    public class IdentityUserRoleConfig : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
               new IdentityUserRole<string>
               {
                   RoleId = "1C1AA946-E470-4D45-9E1B-55C7BB9C4ECE",
                   UserId = "3C0BDB6C-3412-4E27-97AE-909F54967281"
               });
        }
    }
}
