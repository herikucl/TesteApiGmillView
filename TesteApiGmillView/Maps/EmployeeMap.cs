using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TesteApiGmillView.Models;

namespace TesteApiGmillView.Maps
{
    public class EmployeeMap : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("funcionario");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("idfuncionario")
                .UseIdentityColumn();

            builder.Property(x => x.Name)
                .HasColumnName("nome");

            builder.Property(x => x.Document)
                .HasColumnName("documento");

            builder.Property(x => x.Phone)
                .HasColumnName("celular");

            builder.Property(x => x.CompanyId)
                .HasColumnName("idempresa");

            builder.HasOne(x => x.Company)
                .WithMany(x => x.Employees)
                .HasForeignKey(x => x.CompanyId);

            builder.HasMany(x => x.EmployeeProject)
                .WithOne(x => x.Employee)
                .HasForeignKey(x => x.EmployeeId);
        }
    }
}
