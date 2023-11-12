using Excel_Api.Models;
using Excel_Api.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Excel_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly PatientDbContext _context;
        private readonly ILogger<PatientsController> _logger;
        public PatientsController(PatientDbContext context, ILogger<PatientsController> logger)
        {
            _context = context;
            _logger = logger;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Patient>>> GetPatients()
        {
            try
            {
                _logger.LogInformation("Attempting to retrieve patients with related data.");

                var patients = await _context.Patients
                             .Include(p => p.Disease)
                             .Include(p => p.NCDDetails).ThenInclude(nd => nd.NCD)
                             .Include(p => p.AllergiesDetails).ThenInclude(ad => ad.Allergy)
                             .ToListAsync();

                _logger.LogInformation($"Retrieved {patients.Count} patients successfully.");

                foreach (var patient in patients)
                {
                    
                    _logger.LogInformation($"Patient ID: {patient.PatientID}, NCD Details: {patient.NCDDetails.Count}, Allergy Details: {patient.AllergiesDetails.Count}");
                }

                return Ok(patients);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving patients: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        
        // GET: api/Patient/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Patient>> GetPatient(int id)
        {
            var patient = await _context.Patients.FindAsync(id);

            if (patient == null)
            {
                return NotFound();
            }

            return patient;
        }

        // POST: api/Patient
        [HttpPost]
        [Route("insertPatient")]
        public async Task<IActionResult> PostPatient(InsertModel data)
        {
            using (var transaction= _context.Database.BeginTransaction())
            {
                try
                {
                    var patient = new Patient
                    {
                        PatientName = data.PatientName,
                        Epilepsy = data.Epilepsy,
                        DiseaseID = data.DiseaseID,
                    };
                    _context.Patients.Add(patient);
                    await _context.SaveChangesAsync();
                    foreach(var ncdId in data.NCD_Ids)
                    {
                        var ncdDetails = new NCD_Details
                        {
                            NCDID = ncdId,
                            PatientID = patient.PatientID
                        };
                        _context.NCDDetails.Add(ncdDetails);
                    }
                    foreach(var allergyId in data.Allergy_Ids)
                    {
                        var allergiesDetails = new Allergies_Details
                        {
                            AllergyID = allergyId,
                            PatientID = patient.PatientID
                        };
                        _context.AllergiesDetails.Add(allergiesDetails);
                    }
                    await _context.SaveChangesAsync();
                    transaction.Commit();
                    return Ok();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }
            }
        }

        // PUT: api/Patient/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPatient(int id, [FromForm] Patient patient)
        {
            if (id != patient.PatientID)
            {
                return BadRequest();
            }

            _context.Entry(patient).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Patient/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }

            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PatientExists(int id)
        {
            return _context.Patients.Any(e => e.PatientID == id);
        }

    }
}
