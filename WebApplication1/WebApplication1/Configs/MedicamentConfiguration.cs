﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication1.Entities;
namespace WebApplication1.Configs;

public class MedicamentConfiguration: IEntityTypeConfiguration<Medicament>
{
    public void Configure(EntityTypeBuilder<Medicament> modelBuilder)
    {
        modelBuilder
            .HasKey(x => x.IdMedicament);
        modelBuilder
            .Property(x => x.IdMedicament)
            .ValueGeneratedOnAdd()
            .IsRequired();
        modelBuilder
            .Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();
        modelBuilder
            .Property(x => x.Description)
            .HasMaxLength(100)
            .IsRequired();
        modelBuilder
            .Property(x => x.Type)
            .HasMaxLength(100)
            .IsRequired();
        modelBuilder
            .ToTable("Medicament");
    }
}