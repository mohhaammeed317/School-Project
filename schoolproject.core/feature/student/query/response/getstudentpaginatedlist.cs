namespace schoolproject.core.feature.student.query.response;

public class getstudentpaginatedlist
{
        public int StudID { get; set; }
          
        public string? Name { get; set; }
           
        public string? Address { get; set; }
      
        public string? DepartmentName { get; set; }
    public getstudentpaginatedlist(int StudID, string? Name, string? Address, string? DepartmentName)
    {
        this.StudID = StudID;
        this.Name = Name;
        this.Address = Address;
        this.DepartmentName = DepartmentName;
            
    }
}