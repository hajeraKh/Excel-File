using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Excel_Api.Migrations
{
    public partial class ScriptT : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Allergies",
                columns: table => new
                {
                    AllergyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AllergyName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Allergies", x => x.AllergyID);
                });

            migrationBuilder.CreateTable(
                name: "Diseases",
                columns: table => new
                {
                    DiseaseID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiseaseName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diseases", x => x.DiseaseID);
                });

            migrationBuilder.CreateTable(
                name: "NCDs",
                columns: table => new
                {
                    NCDID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NCDName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NCDs", x => x.NCDID);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    PatientID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Epilepsy = table.Column<int>(type: "int", nullable: false),
                    DiseaseID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.PatientID);
                    table.ForeignKey(
                        name: "FK_Patients_Diseases_DiseaseID",
                        column: x => x.DiseaseID,
                        principalTable: "Diseases",
                        principalColumn: "DiseaseID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AllergiesDetails",
                columns: table => new
                {
                    Allergies_DetailsID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientID = table.Column<int>(type: "int", nullable: false),
                    AllergyID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllergiesDetails", x => x.Allergies_DetailsID);
                    table.ForeignKey(
                        name: "FK_AllergiesDetails_Allergies_AllergyID",
                        column: x => x.AllergyID,
                        principalTable: "Allergies",
                        principalColumn: "AllergyID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AllergiesDetails_Patients_PatientID",
                        column: x => x.PatientID,
                        principalTable: "Patients",
                        principalColumn: "PatientID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NCDDetails",
                columns: table => new
                {
                    NCD_DetailsID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientID = table.Column<int>(type: "int", nullable: false),
                    NCDID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NCDDetails", x => x.NCD_DetailsID);
                    table.ForeignKey(
                        name: "FK_NCDDetails_NCDs_NCDID",
                        column: x => x.NCDID,
                        principalTable: "NCDs",
                        principalColumn: "NCDID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NCDDetails_Patients_PatientID",
                        column: x => x.PatientID,
                        principalTable: "Patients",
                        principalColumn: "PatientID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Allergies",
                columns: new[] { "AllergyID", "AllergyName" },
                values: new object[,]
                {
                    { 1, "Drugs-Penicillin" },
                    { 2, "Drugs-Others" },
                    { 3, "Animals" },
                    { 4, "Food" },
                    { 5, "Ointments" },
                    { 6, "Plants" },
                    { 7, "Sprays" },
                    { 8, "Others" },
                    { 9, "No Allergies" }
                });

            migrationBuilder.InsertData(
                table: "Diseases",
                columns: new[] { "DiseaseID", "DiseaseName" },
                values: new object[,]
                {
                    { 1, "Diabetes" },
                    { 2, "Hypertension" },
                    { 3, "Arthritis" },
                    { 4, "Heart Disease" },
                    { 5, "Respiratory Infections" }
                });

            migrationBuilder.InsertData(
                table: "NCDs",
                columns: new[] { "NCDID", "NCDName" },
                values: new object[,]
                {
                    { 1, "Asthma" },
                    { 2, "Cancer" },
                    { 3, "Disorders of ear" },
                    { 4, "Disorders of eye" },
                    { 5, "Mental illness" },
                    { 6, "Oral health Problems" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AllergiesDetails_AllergyID",
                table: "AllergiesDetails",
                column: "AllergyID");

            migrationBuilder.CreateIndex(
                name: "IX_AllergiesDetails_PatientID",
                table: "AllergiesDetails",
                column: "PatientID");

            migrationBuilder.CreateIndex(
                name: "IX_NCDDetails_NCDID",
                table: "NCDDetails",
                column: "NCDID");

            migrationBuilder.CreateIndex(
                name: "IX_NCDDetails_PatientID",
                table: "NCDDetails",
                column: "PatientID");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_DiseaseID",
                table: "Patients",
                column: "DiseaseID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AllergiesDetails");

            migrationBuilder.DropTable(
                name: "NCDDetails");

            migrationBuilder.DropTable(
                name: "Allergies");

            migrationBuilder.DropTable(
                name: "NCDs");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Diseases");
        }
    }
}
