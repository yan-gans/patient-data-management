namespace WebApplication1.Entities;

public class PrescriptionMedicament
{
    public int Dose { get; set; }
    public string Details { get; set; }
    public int IdMedicament { get; set; }
    public int IdPrescription { get; set; }
    
    public virtual Medicament Medicament { get; }= null!;
    public virtual Prescription Prescription { get; }= null!;
}