using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Data.Mappings
{
    public class TeamMap : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.ToTable("Team");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("TeamId").UseMySqlIdentityColumn();
            builder.Property(x => x.Name).HasColumnName("Name");
            builder.Property(x => x.PrimaryColor).HasColumnName("PrimaryColor");
            builder.Property(x => x.SecondaryColor).HasColumnName("SecondaryColor");
        }
    }
}
