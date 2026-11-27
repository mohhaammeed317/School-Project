using System.Globalization;

namespace schoolproject.data.commons;

public class GeneralLocaliztionEntity
{
    public string Localize(string textEN, string textAr)
    {
        CultureInfo CultureInfo = Thread.CurrentThread.CurrentCulture;
        if (CultureInfo.TwoLetterISOLanguageName.ToLower().Equals("ar"))
            return textAr;
        return textEN;
    }
}