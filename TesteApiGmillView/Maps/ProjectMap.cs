using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TesteApiGmillView.Models;

namespace TesteApiGmillView.Maps
{
    public class ProjectMap : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.ToTable("projeto");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("idprojeto")
                .UseIdentityColumn();

            builder.Property(x => x.Name)
                .HasColumnName("nome");

            builder.Property(x => x.Status)
                .HasColumnName("status");

            builder.Property(x => x.Description)
                .HasColumnName("descricao");

            builder.Property(x => x.CompanyId)
                .HasColumnName("idempresa");

            builder.HasOne(x => x.Company)
                .WithMany(x => x.Projects);

            builder.HasMany(x => x.EmployeeProject)
                .WithOne(x => x.Project)
                .HasForeignKey(x => x.ProjectId);
        }
    }
}
