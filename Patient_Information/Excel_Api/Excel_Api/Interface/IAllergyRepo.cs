using Excel_Api.Models;

namespace Excel_Api.Interface
{
        public interface IAllergyRepo
        {
            Task<IEnumerable<Allergy>> GetAll();
            Task<Allergy> GetById(int id);
            Task<Allergy> Insert(Allergy e);
            Task<Allergy> Update(Allergy e);
            Task Delete(int id);
            Task Save();
            Task<Allergy> GetByNameAsync(string AllergyName);
        }
}
