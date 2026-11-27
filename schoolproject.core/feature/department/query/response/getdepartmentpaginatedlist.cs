namespace schoolproject.core.feature.department.query.response;

public class getdepartmentpaginatedlist
{
    public int DID { get; set; }
    public string? Name { get; set; }

    public getdepartmentpaginatedlist(int DID, string? Name)
    {
        this.DID = DID;
        this.Name = Name;
    }
}
