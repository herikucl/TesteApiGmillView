using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TesteApiGmillView.Models;

namespace TesteApiGmillView.Maps
{
    public class CompanyMap : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable("empresa");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("idempresa")
                .UseIdentityColumn();

            builder.Property(x => x.Name)
                .HasColumnName("nome");

            builder.Property(x => x.Address)
                .HasColumnName("endereco");

            builder.HasMany(x => x.Employees)
                .WithOne(x => x.Company)
                .HasForeignKey(x => x.CompanyId);

            builder.HasMany(x => x.Projects)
                .WithOne(x => x.Company)
                .HasForeignKey(x => x.CompanyId);
        }
    }
}
