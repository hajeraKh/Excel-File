using Excel_Api.Models;
using Excel_Api.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Excel_Client.Controllers
{
    public class PatientsController : Controller
    {
        private readonly HttpClient _httpClient;

        public PatientsController()
        {


            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://localhost:5139/api/");

        }
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("Patients");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var patients = JsonConvert.DeserializeObject<IEnumerable<Patient>>(json);

                
                foreach (var patient in patients)
                {
                    patient.NCDDetails ??= new List<NCD_Details>();
                    patient.AllergiesDetails ??= new List<Allergies_Details>();

                  
                    foreach (var ncdDetail in patient.NCDDetails)
                    {
                        ncdDetail.NCD ??= new NCD { NCDName = "N/A" };
                    }

                    foreach (var allergyDetail in patient.AllergiesDetails)
                    {
                        allergyDetail.Allergy ??= new Allergy { AllergyName = "N/A" };
                    }
                }

                return View(patients);
            }
            else
            {
                Console.WriteLine("Error: " + response.StatusCode);
                return View(new List<Patient>());
            }

        }

        public async Task<IActionResult> Create()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/Diseases");

                if (response.IsSuccessStatusCode)
                {
                    var diseases = await response.Content.ReadFromJsonAsync<IEnumerable<Disease>>();
                    var model = new InsertModel
                    {
                        Diseases = diseases?.ToList() ?? new List<Disease>()
                    };

                    return View(model);
                }
                else
                {
                    return View(new InsertModel());
                }
            }
            catch (Exception ex)
            {
                
                return View(new InsertModel());
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(InsertModel data)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("insertPatient", data);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index"); 
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error while saving the patient.");
                   
                    var diseases = await _httpClient.GetAsync("api/Diseases")
                        .Result.Content.ReadFromJsonAsync<IEnumerable<Disease>>();
                    data.Diseases = diseases?.ToList() ?? new List<Disease>();
                    return View(data);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error: {ex.Message}");
                
                var diseases = await _httpClient.GetAsync("api/Diseases")
                    .Result.Content.ReadFromJsonAsync<IEnumerable<Disease>>();
                data.Diseases = diseases?.ToList() ?? new List<Disease>();
                return View(data);
            }
        }
    }
}
