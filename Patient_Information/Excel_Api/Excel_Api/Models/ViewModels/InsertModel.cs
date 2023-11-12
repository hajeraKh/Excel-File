using Excel_Api.Models;

namespace Excel_Api.Models.ViewModels
{
    public class InsertModel
    {
        public InsertModel()
        {
            this.NCD_Ids = new List<int>();
            this.Allergy_Ids = new List<int>();
            this.Diseases = new List<Disease>();
            this.SelectedNCDNames = new List<string>();
        }

        public int PatientID { get; set; }
        public string PatientName { get; set; } = null!;
        public Epilepsy Epilepsy { get; set; }
        public int DiseaseID { get; set; }

     

        public List<int> NCD_Ids { get; set; }
        public List<string> SelectedNCDNames { get; set; }
        public List<int> Allergy_Ids { get; set; }
        public List<string> SelectedAllergies { get; set; }


        public List<Disease> Diseases { get; set; }

    }
}
