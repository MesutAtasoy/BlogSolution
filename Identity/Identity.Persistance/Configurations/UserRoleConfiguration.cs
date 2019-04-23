using Identity.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Persistance.Configurations
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.UpdatedDate).HasColumnType("datetime");

            builder.HasOne(d => d.Role)
                                .WithMany(p => p.UserRoles)
                                .HasForeignKey(d => d.RoleId)
                                .OnDelete(DeleteBehavior.ClientSetNull)
                                .HasConstraintName("FK_UserRoles_Roles");

            builder.HasOne(d => d.User)
                                .WithMany(p => p.UserRoles)
                                .HasForeignKey(d => d.UserId)
                                .OnDelete(DeleteBehavior.ClientSetNull)
                                .HasConstraintName("FK_UserRoles_Users");
        }
    }
}

