using Excel_Api.Helper;
using Excel_Api.Interface;
using Excel_Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



namespace Excel_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AllergiesController : ControllerBase
    {
        private readonly PatientDbContext _db;
        private readonly IAllergyRepo _iAllergy;
        public AllergiesController(IAllergyRepo iAllergy, PatientDbContext db)
        {
            _db = db;
            _iAllergy = iAllergy;
        }
        [HttpGet("GetAll")]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                var manuList = await _iAllergy.GetAll();
                return Ok(manuList);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Allergy>> GetById(int id)
        {
            try
            {
                var aller = await _iAllergy.GetById(id);
                if (aller == null)
                {
                    return NotFound();
                }
                return aller;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retriving data from database");
            }
        }
        [HttpPost("Insert")]
        public async Task<object> Insert([FromBody] Allergy obj)
        {
            try
            {
                if (obj == null)
                {
                    return await Task.FromResult(new ResponseModel(ResponseCode.Error, "Data Missing", null));
                }

               
                if (string.IsNullOrWhiteSpace(obj.AllergyName))
                {
                    return await Task.FromResult(new ResponseModel(ResponseCode.Error, "AllergyName is required", null));
                }

                
                var manu = await _iAllergy.GetById(obj.AllergyID);
                if (manu != null)
                {
                    return await Task.FromResult(new ResponseModel(ResponseCode.Error, "Data Already Exists", manu));
                }

               
                var existingAllergy = await _iAllergy.GetByNameAsync(obj.AllergyName);
                if (existingAllergy != null)
                {
                    return await Task.FromResult(new ResponseModel(ResponseCode.Error, "AllergyName already exists", existingAllergy));
                }


               
                if (!IsValidAllergyName(obj.AllergyName))
                {
                    return await Task.FromResult(new ResponseModel(ResponseCode.Error, "Invalid AllergyName format", null));
                }

                var returnManu = await _iAllergy.Insert(obj);
                return await Task.FromResult(new ResponseModel(ResponseCode.OK, "Data Inserted Successfully", returnManu));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        private bool IsValidAllergyName(string AllergyName)
        {
            if (AllergyName.Length > 100 || AllergyName.Contains("!"))
            {
                return false;
            }

            return true;
        }
        [HttpPut("Update")]
        public async Task<object> Update([FromBody] Allergy obj)
        {
            try
            {
                var test = await _iAllergy.GetById(obj.AllergyID);
                if (test == null)
                {
                    return await Task.FromResult(new ResponseModel(ResponseCode.Error, "Data Object Missing", null));
                }
                var returnManu = await _iAllergy.Update(obj);
                return await Task.FromResult(new ResponseModel(ResponseCode.OK, "Data updated successfully", returnManu));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var allgDelete = await _iAllergy.GetById(id);
                if (allgDelete == null)
                {
                    return NotFound();
                }
                await _iAllergy.Delete(id);
                return Ok("Data Delete Successfully!!");

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retriving data from database");
            }
        }
        
    }
}
