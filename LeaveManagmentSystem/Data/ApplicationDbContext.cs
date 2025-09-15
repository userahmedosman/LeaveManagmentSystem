using LeaveManagmentSystem.Configrations;
using LeaveManagmentSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagmentSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            builder.Entity<IdentityRole>().HasData(
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
            var hasher = new PasswordHasher<ApplicationUser>();
            builder.Entity<ApplicationUser>().HasData(
                new ApplicationUser
                {
                    Id = "3C0BDB6C-3412-4E27-97AE-909F54967281",
                    FirstName = "Ahmed",
                    LastName = "Osman",
                    BirthDate= new DateOnly(1998,12,19),
                    Email = "ahmedhaj000@gmail.com",
                    NormalizedEmail = "AHMEDHAJ000@GMAIL.COM",
                    NormalizedUserName = "AHMEDHAJ000@GMAIL.COM",
                    UserName = "ahmedhaj000@gmail.com",
                    PasswordHash = hasher.HashPassword(null, "Hajo1!"),
                    EmailConfirmed = true,
                    isDeleted = false,
                    DeletedAt = null
                });

            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> {
                RoleId = "1C1AA946-E470-4D45-9E1B-55C7BB9C4ECE",
                UserId = "3C0BDB6C-3412-4E27-97AE-909F54967281"
                });

        }

        public DbSet<LeaveType> LeaveTypes { get; set; }
    }
}
