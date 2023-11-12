using Excel_Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Excel_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NCDSController : ControllerBase
    {
        private readonly PatientDbContext _context;

        public NCDSController(PatientDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NCD>>> GetNCD()
        {
            return await _context.NCDs.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<NCD>> GetNCD(int id)
        {
            var ncd = await _context.NCDs.FindAsync(id);

            if (ncd == null)
            {
                return NotFound();
            }

            return ncd;
        }
        [HttpPost]
        public async Task<ActionResult<NCD>> PostNCD( [FromForm]NCD ncd)
        {
            _context.NCDs.Add(ncd);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNCD", new { id = ncd.NCDID }, ncd);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNCD(int id, [FromForm] NCD ncd)
        {
            if (id != ncd.NCDID)
            {
                return BadRequest();
            }

            _context.Entry(ncd).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NCDExists(id))
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
        public async Task<IActionResult> DeleteNCD(int id)
        {
            var ncd = await _context.NCDs.FindAsync(id);
            if (ncd == null)
            {
                return NotFound();
            }

            _context.NCDs.Remove(ncd);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NCDExists(int id)
        {
            return _context.NCDs.Any(e => e.NCDID == id);
        }

    }
}
