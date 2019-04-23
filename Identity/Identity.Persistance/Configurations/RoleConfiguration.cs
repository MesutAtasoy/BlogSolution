using Identity.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Persistance.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.Property(e => e.Id).HasDefaultValueSql("(newid())");

            builder.Property(e => e.CreatedDate)
                                .HasColumnType("datetime")
                                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.Name)
                                .IsRequired()
                                .HasMaxLength(250);

            builder.Property(e => e.UpdatedDate).HasColumnType("datetime");
        }
    }
}


