using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeaveManagmentSystem.Configrations
{
    public class IdentityRoleConfig : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
               new IdentityRole
               {
                   Id = "5B65FA0F-8691-4C82-8241-DA2BF3A1FE0F",
                   Name = "Employee",
                   NormalizedName = "EMPLOYEE"
               },
               new IdentityRole
               {
                   Id = "BB70A46B-19D5-4872-8911-666B6777CE84",
                   Name = "Supervisor",
                   NormalizedName = "SUPERVISOR"
               },
               new IdentityRole
               {
                   Id = "1C1AA946-E470-4D45-9E1B-55C7BB9C4ECE",
                   Name = "Admin",
                   NormalizedName = "ADMIN"
               }

           );
        }
    }
}
