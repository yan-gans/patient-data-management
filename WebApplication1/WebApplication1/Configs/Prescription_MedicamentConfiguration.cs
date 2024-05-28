using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication1.Entities;
namespace WebApplication1.Configs;

public class Prescription_MedicamentConfiguration
{
    public void Configure(EntityTypeBuilder<Prescription_Medicament> modelBuilder)
    {
        modelBuilder
            .HasKey(x => x.IdPrescription);
        modelBuilder
            .Property(x => x.IdPrescription)
            .ValueGeneratedOnAdd()
            .IsRequired();
        modelBuilder
            .ToTable("Prescription");
        modelBuilder
            .Property(x => x.Details)
            .HasMaxLength(100)
            .IsRequired();
        modelBuilder
            .HasOne(x => x.Medicament)
            .WithMany(x => x.Prescription_Meds)
            .HasForeignKey(x => x.IdMedicament);
        
        modelBuilder
            .HasOne(x => x.Patient)
            .WithMany(x => x.Prescriptions)
            .HasForeignKey(x => x.IdPatient);
    }
}