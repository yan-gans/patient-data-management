using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication1.Entities;
namespace WebApplication1.Configs;

public class PrescriptionMedicamentConfiguration: IEntityTypeConfiguration<PrescriptionMedicament>
{
    public void Configure(EntityTypeBuilder<PrescriptionMedicament> modelBuilder)
    {
        modelBuilder
            .HasKey(x => new {x.IdPrescription,x.IdMedicament}).HasName("Prescription_Medicament_pk");
        modelBuilder
            .Property(x => x.IdPrescription)
            .ValueGeneratedOnAdd()
            .IsRequired();
        modelBuilder
            .ToTable("Prescription_Medicament");
        modelBuilder
            .Property(x => x.Details)
            .HasMaxLength(100)
            .IsRequired();
        modelBuilder
            .HasOne(x => x.Medicament)
            .WithMany(x => x.Prescription_Meds)
            .HasForeignKey(x => x.IdMedicament);
        
        modelBuilder
            .HasOne(x => x.Prescription)
            .WithMany(x => x.Prescription_Meds)
            .HasForeignKey(x => x.IdPrescription);
    }
}