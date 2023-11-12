using Excel_Api.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Excel_Client.Models.ViewModels
{
    public class CreatePatientViewModel
    {
        public string PatientName { get; set; }
        public Epilepsy Epilepsy { get; set; }
        public int DiseaseID { get; set; } 
        public SelectList Diseases { get; set; }

        public SelectList PatientNameList { get; set; }
        public SelectList EpilepsyList { get; set; }
        public SelectList DiseaseList { get; set; }
    }
}
