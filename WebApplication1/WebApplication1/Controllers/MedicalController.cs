using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Context;
using WebApplication1.DTO;
using WebApplication1.Entities;

namespace WebApplication1.Controllers;

[Route("medical_api")]
[ApiController]
public class MedicalController : ControllerBase
{
    private readonly MedicalContext _context;
    public MedicalController(MedicalContext context)
    {
        this._context = context;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePrescription(PrescriptionCreate prescriptionCreate,CancellationToken token)
    {
        if (!await _context.Patients.AnyAsync(x => x.IdPatient == prescriptionCreate.Patient.IdPatient,token) ||
            !await _context.Patients.AnyAsync(x => x.FirstName == prescriptionCreate.Patient.FirstName&&
                                                   x.LastName==prescriptionCreate.Patient.LastName&&x.Birthdate==prescriptionCreate.Patient.Birthdate,token))
        {
            var p = prescriptionCreate.Patient;
            var patient = new Patient
            {
                FirstName = p.FirstName,
                LastName = p.LastName,
                Birthdate = p.Birthdate
            };
            await _context.Patients.AddAsync(patient,token);
            await _context.SaveChangesAsync(token);
        }
        if (!await _context.Doctors.AnyAsync(x => x.IdDoctor == prescriptionCreate.Doctor.IdDoctor,token))
        {
            var d = prescriptionCreate.Doctor;
            var doctor = new Doctor
            {
                FirstName = d.FirstName,
                LastName = d.LastName,
                Email = d.Email
            };
            await _context.Doctors.AddAsync(doctor,token);
            await _context.SaveChangesAsync(token);
        }
        if (! prescriptionCreate.Medicaments.Any(x => _context.Medicaments.Any(m => m.IdMedicament == x.IdMedicament))||prescriptionCreate.Medicaments.Count>10||prescriptionCreate.Date>prescriptionCreate.DueDate)
            return BadRequest();
        var presc = new Prescription
        {
            Date = prescriptionCreate.Date,
            DueDate = prescriptionCreate.DueDate,
            IdPatient = prescriptionCreate.Patient.IdPatient,
            IdDoctor = prescriptionCreate.Doctor.IdDoctor
        };
        await _context.Prescriptions.AddAsync(presc,token);
        await _context.SaveChangesAsync(token);
        foreach (var m in prescriptionCreate.Medicaments)
        {
            var pm = new PrescriptionMedicament
            {
                Dose = m.Dose,
                Details = m.Details,
                IdPrescription = presc.IdPrescription,
                IdMedicament = m.IdMedicament
            };
            await _context.PrescriptionMedicaments.AddAsync(pm,token);
            await _context.SaveChangesAsync(token);
        }
        return Ok("Created");
    }

    [HttpGet("/patients{idPatient}")]
    public async Task<IActionResult> DeleteClient(int idPatient, CancellationToken token)
    {
        var patient = await _context.Patients
            .Where(x => x.IdPatient == idPatient)
            .Include(x => x.Prescriptions)
            .ThenInclude(p => p.Doctor) 
            .Include(x => x.Prescriptions)
            .ThenInclude(p => p.Prescription_Meds)
            .ThenInclude(m => m.Medicament)
            .FirstOrDefaultAsync(token);
        if (patient == null)
            return NotFound();
        return Ok(patient);
    }
    
    
}