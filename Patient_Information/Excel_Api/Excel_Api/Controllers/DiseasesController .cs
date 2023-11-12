using Excel_Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Excel_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiseasesController : ControllerBase
    {
        private readonly PatientDbContext _context;

        public DiseasesController(PatientDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Disease>>> GetDiseases()
        {
            return await _context.Diseases.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Disease>> GetDisease(int id)
        {
            var disease = await _context.Diseases.FindAsync(id);

            if (disease == null)
            {
                return NotFound();
            }

            return disease;
        }

       
        [HttpPost]
        public async Task<ActionResult<Disease>> PostDisease([FromForm]Disease disease)
        {
            _context.Diseases.Add(disease);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDisease", new { id = disease.DiseaseID }, disease);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDisease(int id,[FromForm] Disease disease)
        {
            if (id != disease.DiseaseID)
            {
                return BadRequest();
            }

            _context.Entry(disease).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DiseaseExists(id))
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
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDisease(int id)
        {
            var disease = await _context.Diseases.FindAsync(id);
            if (disease == null)
            {
                return NotFound();
            }

            _context.Diseases.Remove(disease);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DiseaseExists(int id)
        {
            return _context.Diseases.Any(e => e.DiseaseID == id);
        }
    }
}
