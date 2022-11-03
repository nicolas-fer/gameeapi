using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Data.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("UserId").UseMySqlIdentityColumn();
            builder.Property(x => x.Username).HasColumnName("Username");
            builder.Property(x => x.Password).HasColumnName("Password");
            builder.Property(x => x.Role).HasColumnName("Role");
        }
    }
}
