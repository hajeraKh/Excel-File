using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UML_Class
{
    using System;

 
    public class Clinician
    {
      
        public string Name { get; set; }
        public string HospitalName { get; set; }

     
        public bool Login(string username, string password)
        {
           
            return true; 
        }

     
        private bool IsSessionExists(string username)
        {
            
            return true; 
        }
    }

  
    public class Doctor : Clinician
    {
     
        public string PracticeNumber { get; set; }

      
        public void CreatePrescription(int patientNumber)
        {
          
            Console.WriteLine($"Prescription created for patient {patientNumber}");
        }
    }


    public class Pharmacist : Clinician
    {
      
        public string PharmacistNumber { get; set; }

      
        public void DispenseMedications(int prescriptionNumber)
        {
          
            Console.WriteLine($"Medications dispensed for prescription {prescriptionNumber}");
        }
    }

    class Program
    {
        static void Main()
        {
         
            Doctor doctor = new Doctor
            {
                Name = "Tareq",
                HospitalName = "Insaf Hospital",
                PracticeNumber = "3456"
            };

            Pharmacist pharmacist = new Pharmacist
            {
                Name = "Rohan",
                HospitalName = "Insaf Hospital",
                PharmacistNumber = "3458"
            };

          
            doctor.CreatePrescription(101);
            pharmacist.DispenseMedications(201);
            Console.ReadKey();
        }
    }

}
