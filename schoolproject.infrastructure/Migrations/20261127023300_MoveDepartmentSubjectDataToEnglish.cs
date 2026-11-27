using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace schoolproject.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MoveDepartmentSubjectDataToEnglish : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
UPDATE [departments]
SET [DNameen] = CASE WHEN [DNameen] = N'' THEN [DNamear] ELSE [DNameen] END,
    [DNamear] = N'';

UPDATE [subjects]
SET [SubjectNameen] = CASE WHEN [SubjectNameen] = N'' THEN [SubjectNamear] ELSE [SubjectNameen] END,
    [SubjectNamear] = N'';
");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
UPDATE [departments]
SET [DNamear] = [DNameen],
    [DNameen] = N'';

UPDATE [subjects]
SET [SubjectNamear] = [SubjectNameen],
    [SubjectNameen] = N'';
");
        }
    }
}
