using MediatR;
using schoolproject.core.bases;

namespace schoolproject.core.feature.student.command.model;

public class editstudent :IRequest<Response<string>>
{
    public editstudent()
    {
        
        
    }
    public int id { get; set; }
    
    public string Nameen { get; set; }
    public string Namear { get; set; }

    public string Addressar { get; set; }
    public string Addressen { get; set; }

    public string phone_number { get; set; }
    
    public int department_id { get; set; }
}