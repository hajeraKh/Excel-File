using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Excel_Api.Models
{
    public enum Epilepsy
    {
        Yes = 1,
        No

    }
    public class Patient
    {
        [Key]
        public int PatientID { get; set; }
        public string PatientName { get; set; }

        [EnumDataType(typeof(Epilepsy))]
        public Epilepsy Epilepsy { get; set; }
        [ForeignKey("DiseaseID")]
        public int DiseaseID { get; set; }
        public virtual Disease? Disease { get; set; }
        [JsonIgnore]
        public virtual ICollection<NCD_Details>? NCDDetails { get; set; } = new List<NCD_Details>();
        [JsonIgnore]
        public virtual ICollection<Allergies_Details>? AllergiesDetails { get; set; } = new List<Allergies_Details>();
    }

    public class Disease
    {
        [Key]
        public int DiseaseID { get; set; }
        public string DiseaseName { get; set; }
        [JsonIgnore]
        public virtual ICollection<Patient>? Patients { get; } = new List<Patient>();
    }

 
    public class NCD
    {
        [Key]
        public int NCDID { get; set; }
        public string NCDName { get; set; }
        [JsonIgnore]
        public virtual ICollection<NCD_Details>? NCD_Details { get; } = new List<NCD_Details>();
    }

    public class Allergy
    {
        [Key]
        public int AllergyID { get; set; }
        public string AllergyName { get; set; }
        [JsonIgnore]
        public virtual ICollection<Allergies_Details>? Allergies_Details { get; } = new List<Allergies_Details>();
    }

    public class NCD_Details
    {
        [Key]
        public int NCD_DetailsID { get; set; }

       
        public int PatientID { get; set; }
        public int NCDID { get; set; }

        [ForeignKey("PatientID")]
        public virtual Patient? Patient { get; set; }
        [ForeignKey("NCDID")]
        public virtual NCD? NCD { get; set; }
    }

    public class Allergies_Details
    {
        [Key]
        public int Allergies_DetailsID { get; set; }
        public int PatientID { get; set; }
        public int AllergyID { get; set; }

        [ForeignKey("PatientID")]
        public virtual Patient? Patient { get; set; }

        [ForeignKey("AllergyID")]
        public virtual Allergy? Allergy { get; set; }
    }
    public class PatientDbContext : DbContext
    {
        public PatientDbContext(DbContextOptions<PatientDbContext> options) : base(options)
        {
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Disease> Diseases { get; set; }
        public DbSet<NCD> NCDs { get; set; }
        public DbSet<Allergy> Allergies { get; set; }
        public DbSet<NCD_Details> NCDDetails { get; set; }
        public DbSet<Allergies_Details> AllergiesDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Seed NCD data
            modelBuilder.Entity<NCD>().HasData(
                new NCD { NCDID = 1, NCDName = "Asthma" },
                new NCD { NCDID = 2, NCDName = "Cancer" },
                new NCD { NCDID = 3, NCDName = "Disorders of ear" },
                new NCD { NCDID = 4, NCDName = "Disorders of eye" },
                new NCD { NCDID = 5, NCDName = "Mental illness" },
                new NCD { NCDID = 6, NCDName = "Oral health Problems" }
            );
            modelBuilder.Entity<Disease>().HasData(
                new Disease { DiseaseID = 1, DiseaseName = "Diabetes" },
                new Disease { DiseaseID = 2, DiseaseName = "Hypertension" },
                new Disease { DiseaseID = 3, DiseaseName = "Arthritis" },
                new Disease { DiseaseID = 4, DiseaseName = "Heart Disease" },
                new Disease { DiseaseID = 5, DiseaseName = "Respiratory Infections" }
            );
            //modelBuilder.Entity<Patient>().HasData(
            //  new Patient { PatientID = 1, PatientName = "Mahin", Epilepsy = Epilepsy.Yes },
            //  new Patient { PatientID = 2, PatientName = "Fahim", Epilepsy = Epilepsy.No },
            //  new Patient { PatientID = 3, PatientName = "Mahin", Epilepsy = Epilepsy.Yes }
            //  );

            // Seed Allergies data
            modelBuilder.Entity<Allergy>().HasData(
                new Allergy { AllergyID = 1, AllergyName = "Drugs-Penicillin" },
                new Allergy { AllergyID = 2, AllergyName = "Drugs-Others" },
                new Allergy { AllergyID = 3, AllergyName = "Animals" },
                new Allergy { AllergyID = 4, AllergyName = "Food" },
                new Allergy { AllergyID = 5, AllergyName = "Ointments" },
                new Allergy { AllergyID = 6, AllergyName = "Plants" },
                new Allergy { AllergyID = 7, AllergyName = "Sprays" },
                new Allergy { AllergyID = 8, AllergyName = "Others" },
                new Allergy { AllergyID = 9, AllergyName = "No Allergies" }
            );

          
        }
    }
}
