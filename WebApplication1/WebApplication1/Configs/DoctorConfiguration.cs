using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication1.Entities;

namespace WebApplication1.Configs;

public class DoctorConfiguration: IEntityTypeConfiguration<Doctor>
{
    public void Configure(EntityTypeBuilder<Doctor> modelBuilder)
    {
        modelBuilder
            .HasKey(x => x.IdDoctor);
        modelBuilder
            .Property(x => x.IdDoctor)
            .ValueGeneratedOnAdd()
            .IsRequired();
        modelBuilder
            .Property(x => x.FirstName)
            .HasMaxLength(100)
            .IsRequired();
        modelBuilder
            .Property(x => x.LastName)
            .HasMaxLength(100)
            .IsRequired();
        modelBuilder
            .Property(x => x.Email)
            .HasMaxLength(100)
            .IsRequired();
        modelBuilder
            .ToTable("Doctor");
    }
}