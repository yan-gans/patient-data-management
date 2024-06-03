using Microsoft.EntityFrameworkCore;
using WebApplication1.Configs;
using WebApplication1.Entities;

namespace WebApplication1.Context;

public class MedicalContext: DbContext
{
    public MedicalContext(DbContextOptions options): base(options)
    {
        
    }
    public MedicalContext()
    {
        
    }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MedicalContext).Assembly);
    }
}