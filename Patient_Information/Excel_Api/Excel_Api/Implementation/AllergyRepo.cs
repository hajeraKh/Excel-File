using Excel_Api.Interface;
using Excel_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Excel_Api.Implementation
{
    public class AllergyRepo : IAllergyRepo
    {
        private readonly PatientDbContext _context;
        public AllergyRepo(PatientDbContext context)
        {
            this._context = context;
        }
        public async Task Delete(int id)
        {
            var result = await _context.Allergies.FirstOrDefaultAsync(p => p.AllergyID == id);
            if (result != null)
            {
                _context.Allergies.Remove(result);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Allergy>> GetAll()
        {
            IEnumerable<Allergy> allergies = await _context.Allergies.Select(e => new Allergy
            {
                AllergyID = e.AllergyID,
                AllergyName = e.AllergyName,
            }).ToListAsync();
            return allergies;
        }

        public async Task<Allergy> GetById(int id)
        {
            Allergy? e = await _context.Allergies.AsNoTracking()
                          .FirstOrDefaultAsync(e => e.AllergyID == id);
            if (e != null)
            {
                Allergy manu = new Allergy
                {
                    AllergyID = e.AllergyID,
                    AllergyName = e.AllergyName,
                };
                return manu;
            }
            return null;
        }

        public async Task<Allergy> GetByNameAsync(string AllergyName)
        {
            Allergy? e = await _context.Allergies.AsNoTracking()
                         .FirstOrDefaultAsync(e => e.AllergyName == AllergyName);


            if (e != null)
            {
                Allergy manu = new Allergy
                {
                    AllergyID = e.AllergyID,
                    AllergyName = e.AllergyName,


                };
                return manu;
            }
            return null;
        }

        public async Task<Allergy> Insert(Allergy e)
        {
            Allergy manuObj = new Allergy();
            if (e != null)
            {
                Allergy obj = new Allergy()
                {
                    AllergyID = e.AllergyID,
                    AllergyName = e.AllergyName,

                };
                await _context.Allergies.AddAsync(obj);
                await Save();
                manuObj = await GetById(obj.AllergyID);

            }
            return manuObj;
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Allergy> Update(Allergy e)
        {
            var facturer = await _context.Allergies.FirstOrDefaultAsync(h => h.AllergyID == e.AllergyID);
            Allergy manu = new Allergy();
            if (facturer != null)
            {
                facturer.AllergyID = e.AllergyID;

                facturer.AllergyName = e.AllergyName;
            }
            await Save();
            manu = await GetById(facturer.AllergyID);
            return manu;
        }
    }
}
