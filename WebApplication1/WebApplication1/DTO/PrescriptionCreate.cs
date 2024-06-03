using WebApplication1.Entities;

namespace WebApplication1.DTO;

public class PrescriptionCreate
{
    public PatientDTO Patient { get; set; }
    public DoctorDTO Doctor { get; set; }
    public ICollection<MedicamentDTO> Medicaments { get; set; }
    public DateTime Date{ get; set; }
    public DateTime DueDate{ get; set; }
}