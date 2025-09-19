using LeaveManagmentSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeaveManagmentSystem.Configrations
{
    public class LeaveRequestStatusConfig : IEntityTypeConfiguration<LeaveRequestStatus>
    {
        public void Configure(EntityTypeBuilder<LeaveRequestStatus> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).IsRequired();

            builder.Property(x => x.Name).IsRequired();

            builder.HasData(
            new LeaveRequestStatus
            {
                Id = 1,
                Name = "Pending.."
            },
             new LeaveRequestStatus
             {
                 Id = 2,
                 Name = "Approved"
             },
              new LeaveRequestStatus
              {
                  Id = 3,
                  Name = "Declined"
              },
               new LeaveRequestStatus
               {
                   Id = 4,
                   Name = "Canceled"
               }
            );
        }
    }
}
