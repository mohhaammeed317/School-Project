using MediatR;
using schoolproject.core.bases;

namespace schoolproject.core.feature.student.command.model;

public class editstudent :IRequest<Response<string>>
{
    public editstudent()
    {
        
        
    }
    public int id { get; set; }
    
    public string Name { get; set; }

    public string Address { get; set; }

    public string phone_number { get; set; }
    
    public int department_id { get; set; }
}