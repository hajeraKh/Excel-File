namespace Excel_Api.Models.ViewModels
{
    public class PatientWithDetails
    {
        public int PatientID { get; set; }
        public string PatientName { get; set; }
        public string Epilepsy { get; set; }
        public string DiseaseName { get; set; }
        public List<string> NCDName { get; set; }=new List<string>();
        public List<string> AllergyName { get; set; } = new List<string>();
    }
}
