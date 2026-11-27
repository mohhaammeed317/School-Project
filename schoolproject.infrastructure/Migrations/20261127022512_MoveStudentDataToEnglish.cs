using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace schoolproject.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MoveStudentDataToEnglish : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
UPDATE [students]
SET [Nameen] = CASE WHEN [Nameen] = N'' THEN [Namear] ELSE [Nameen] END,
    [Addressen] = CASE WHEN [Addressen] = N'' THEN [Addressar] ELSE [Addressen] END,
    [Namear] = N'',
    [Addressar] = N'';
");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
UPDATE [students]
SET [Namear] = [Nameen],
    [Addressar] = [Addressen],
    [Nameen] = N'',
    [Addressen] = N'';
");
        }
    }
}
