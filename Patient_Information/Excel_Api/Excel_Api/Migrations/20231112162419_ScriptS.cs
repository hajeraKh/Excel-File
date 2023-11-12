using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Excel_Api.Migrations
{
    public partial class ScriptS : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "PatientID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "PatientID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "PatientID",
                keyValue: 3);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "PatientID", "DiseaseID", "Epilepsy", "PatientName" },
                values: new object[] { 1, 0, 1, "Mahin" });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "PatientID", "DiseaseID", "Epilepsy", "PatientName" },
                values: new object[] { 2, 0, 2, "Fahim" });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "PatientID", "DiseaseID", "Epilepsy", "PatientName" },
                values: new object[] { 3, 0, 1, "Mahin" });
        }
    }
}
