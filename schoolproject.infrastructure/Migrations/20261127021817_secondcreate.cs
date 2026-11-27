using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace schoolproject.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class secondcreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SubjectName",
                table: "subjects",
                
                newName: "SubjectNamear");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "students",
                newName: "Namear");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "students",
                newName: "Addressar");

            migrationBuilder.RenameColumn(
                name: "DName",
                table: "departments",
                newName: "DNamear");

            migrationBuilder.AddColumn<string>(
                name: "SubjectNameen",
                table: "subjects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Addressen",
                table: "students",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Nameen",
                table: "students",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DNameen",
                table: "departments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubjectNameen",
                table: "subjects");

            migrationBuilder.DropColumn(
                name: "Addressen",
                table: "students");

            migrationBuilder.DropColumn(
                name: "Nameen",
                table: "students");

            migrationBuilder.DropColumn(
                name: "DNameen",
                table: "departments");

            migrationBuilder.RenameColumn(
                name: "SubjectNamear",
                table: "subjects",
                newName: "SubjectName");

            migrationBuilder.RenameColumn(
                name: "Namear",
                table: "students",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Addressar",
                table: "students",
                newName: "Address");

            migrationBuilder.RenameColumn(
                name: "DNamear",
                table: "departments",
                newName: "DName");
        }
    }
}
