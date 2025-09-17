using LeaveManagmentSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeaveManagmentSystem.Configrations
{
    public class LeaveAllocationConfig : IEntityTypeConfiguration<LeaveAllocation>
    {
        public void Configure(EntityTypeBuilder<LeaveAllocation> builder)
        {
            builder.HasIndex(la => la.Id);

            builder.Property(la => la.Days);

           
        }
    }
}
