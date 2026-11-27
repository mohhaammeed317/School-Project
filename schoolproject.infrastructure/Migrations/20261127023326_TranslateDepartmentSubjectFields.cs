using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace schoolproject.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TranslateDepartmentSubjectFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
UPDATE [departments]
SET [DNamear] = CASE LTRIM(RTRIM([DNameen]))
        WHEN N'Artificial Intelligence' THEN N'الذكاء الاصطناعي'
        WHEN N'Computer Science' THEN N'علوم الحاسب'
        WHEN N'Information Systems' THEN N'نظم المعلومات'
        WHEN N'Information Technology' THEN N'تكنولوجيا المعلومات'
        ELSE [DNamear]
    END;

UPDATE [subjects]
SET [SubjectNamear] = CASE LTRIM(RTRIM([SubjectNameen]))
        WHEN N'Artificial Intelligence' THEN N'الذكاء الاصطناعي'
        WHEN N'Computer Networks' THEN N'شبكات الحاسوب'
        WHEN N'Data Structures' THEN N'هياكل البيانات'
        WHEN N'Database Systems' THEN N'أنظمة قواعد البيانات'
        WHEN N'Object Oriented Programming' THEN N'البرمجة كائنية التوجه'
        WHEN N'Operating Systems' THEN N'أنظمة التشغيل'
        ELSE [SubjectNamear]
    END;
");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
UPDATE [departments]
SET [DNameen] = CASE LTRIM(RTRIM([DNamear]))
        WHEN N'الذكاء الاصطناعي' THEN N'Artificial Intelligence'
        WHEN N'علوم الحاسب' THEN N'Computer Science'
        WHEN N'نظم المعلومات' THEN N'Information Systems'
        WHEN N'تكنولوجيا المعلومات' THEN N'Information Technology'
        ELSE [DNameen]
    END,
    [DNamear] = N'';

UPDATE [subjects]
SET [SubjectNameen] = CASE LTRIM(RTRIM([SubjectNamear]))
        WHEN N'الذكاء الاصطناعي' THEN N'Artificial Intelligence'
        WHEN N'شبكات الحاسوب' THEN N'Computer Networks'
        WHEN N'هياكل البيانات' THEN N'Data Structures'
        WHEN N'أنظمة قواعد البيانات' THEN N'Database Systems'
        WHEN N'البرمجة كائنية التوجه' THEN N'Object Oriented Programming'
        WHEN N'أنظمة التشغيل' THEN N'Operating Systems'
        ELSE [SubjectNameen]
    END,
    [SubjectNamear] = N'';
");
        }
    }
}
