using Identity.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Persistance.Configurations
{
    public class UserPasswordRequestConfiguration : IEntityTypeConfiguration<UserPasswordRequest>
    {
        public void Configure(EntityTypeBuilder<UserPasswordRequest> builder)
        {
            builder.Property(e => e.Id).HasDefaultValueSql("(newid())");

            builder.Property(e => e.ActivationCode).HasDefaultValueSql("(newid())");

            builder.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.ExpiredDate).HasColumnType("datetime");

            builder.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");

            builder.Property(e => e.UpdatedDate).HasColumnType("datetime");

            builder.Property(e => e.UsedDate).HasColumnType("datetime");

            builder.HasOne(d => d.User)
                .WithMany(p => p.UserPasswordRequests)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserPasswordRequests_Users");
        }
    }
}
