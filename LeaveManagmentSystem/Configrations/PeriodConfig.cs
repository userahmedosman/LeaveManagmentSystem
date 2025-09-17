using LeaveManagmentSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeaveManagmentSystem.Configrations
{
    public class PeriodConfig : IEntityTypeConfiguration<Period>
    {
        public void Configure(EntityTypeBuilder<Period> builder)
        {
            builder.HasIndex(p => p.Id);

            builder.Property(p => p.Name);

            builder.Property(p => p.StartDate);

            builder.Property(p => p.EndDate);
        }
    }
}
