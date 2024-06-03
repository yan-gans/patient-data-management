namespace WebApplication1.Entities;

public class Prescription
{
    public int IdPrescription { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public int IdDoctor { get; set; }
    public int IdPatient { get; set; }
    
    public virtual Doctor Doctor { get; }= null!;
    public virtual Patient Patient { get; }= null!;
    public virtual ICollection<PrescriptionMedicament> Prescription_Meds { get; }= null!;

}