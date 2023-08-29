using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TesteApiGmillView.Models;

namespace TesteApiGmillView.Maps
{
    public class EmployeeProjectMap : IEntityTypeConfiguration<EmployeeProject>
    {
        public void Configure(EntityTypeBuilder<EmployeeProject> builder)
        {
            builder.ToTable("funcionario_projeto");

            builder.HasKey(e => e.ProjectId);
            builder.Property(e => e.ProjectId)
                .HasColumnName("idprojeto")
                .UseIdentityColumn();

            builder.HasKey(e => e.EmployeeId);
            builder.Property(e => e.EmployeeId)
                .HasColumnName("idfuncionario")
                .UseIdentityColumn();
        }
    }
}
