﻿namespace WebApplication1.Entities;

public class Medicament
{
    public int IdMedicament { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Type { get; set; }
    public virtual ICollection<PrescriptionMedicament> Prescription_Meds { get; }= null!;
}