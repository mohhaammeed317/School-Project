namespace schoolproject.core.feature.subject.query.response;

public class getsubjectpaginatedlist
{
    public int SubID { get; set; }
    public string? Name { get; set; }
    public DateTime Period { get; set; }

    public getsubjectpaginatedlist(int SubID, string? Name, DateTime Period)
    {
        this.SubID = SubID;
        this.Name = Name;
        this.Period = Period;
    }
}
