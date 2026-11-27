using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace schoolproject.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FillStudentArabicFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
UPDATE [students]
SET [Namear] = CASE LTRIM(RTRIM([Nameen]))
        WHEN N'Ahmed Mohamed' THEN N'أحمد محمد'
        WHEN N'Mahmoud Ali' THEN N'محمود علي'
        WHEN N'Omar Hassan' THEN N'عمر حسن'
        WHEN N'Omar Ibrahim' THEN N'عمر إبراهيم'
        WHEN N'Mohamed Hassan' THEN N'محمد حسن'
        WHEN N'Mostafa Ibrahim' THEN N'مصطفى إبراهيم'
        WHEN N'Youssef Khaled' THEN N'يوسف خالد'
        WHEN N'Abdullah Samir' THEN N'عبد الله سمير'
        WHEN N'karim' THEN N'كريم'
        WHEN N'mohsen' THEN N'محسن'
        WHEN N'patates' THEN N'بطاطس'
        ELSE [Namear]
    END,
    [Addressar] = CASE LTRIM(RTRIM([Addressen]))
        WHEN N'Cairo' THEN N'القاهرة'
        WHEN N'Giza' THEN N'الجيزة'
        WHEN N'Alexandria' THEN N'الإسكندرية'
        WHEN N'luxor' THEN N'الأقصر'
        WHEN N'matrouh' THEN N'مطروح'
        WHEN N'el marg' THEN N'المرج'
        ELSE [Addressar]
    END;
");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
UPDATE [students]
SET [Nameen] = CASE LTRIM(RTRIM([Namear]))
        WHEN N'أحمد محمد' THEN N'Ahmed Mohamed'
        WHEN N'محمود علي' THEN N'Mahmoud Ali'
        WHEN N'عمر حسن' THEN N'Omar Hassan'
        WHEN N'عمر إبراهيم' THEN N'Omar Ibrahim'
        WHEN N'محمد حسن' THEN N'Mohamed Hassan'
        WHEN N'مصطفى إبراهيم' THEN N'Mostafa Ibrahim'
        WHEN N'يوسف خالد' THEN N'Youssef Khaled'
        WHEN N'عبد الله سمير' THEN N'Abdullah Samir'
        WHEN N'كريم' THEN N'karim'
        WHEN N'محسن' THEN N'mohsen'
        WHEN N'بطاطس' THEN N'patates'
        ELSE [Nameen]
    END,
    [Addressen] = CASE LTRIM(RTRIM([Addressar]))
        WHEN N'القاهرة' THEN N'Cairo'
        WHEN N'الجيزة' THEN N'Giza'
        WHEN N'الإسكندرية' THEN N'Alexandria'
        WHEN N'الأقصر' THEN N'luxor'
        WHEN N'مطروح' THEN N'matrouh'
        WHEN N'المرج' THEN N'el marg'
        ELSE [Addressen]
    END,
    [Namear] = N'',
    [Addressar] = N'';
");
        }
    }
}
