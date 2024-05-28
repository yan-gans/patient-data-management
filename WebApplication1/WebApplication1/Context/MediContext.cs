using Microsoft.EntityFrameworkCore;
using WebApplication1.Configs;
using WebApplication1.Entities;

namespace WebApplication1.Context;

public class MediContext:DbContext
{
    protected MediContext(DbContextOptions options): base(options)
    {
        
    }

    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new PatientConfiguration());
        modelBuilder.ApplyConfiguration(new DoctorConfiguration());
        modelBuilder.ApplyConfiguration(new PrescriptionConfiguration());
    }
}