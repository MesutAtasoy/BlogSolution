using Identity.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Persistance.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(e => e.Id).HasDefaultValueSql("(newid())");

            builder.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.Email).HasMaxLength(150);

            builder.Property(e => e.Name).HasMaxLength(250);

            builder.Property(e => e.PhoneNumber).HasMaxLength(50);

            builder.Property(e => e.UpdatedDate).HasColumnType("datetime");

            builder.Property(e => e.Username).HasMaxLength(250);
        }
    }
}
