using System.ComponentModel.DataAnnotations;
using MediatR;
using schoolproject.core.bases;

namespace schoolproject.core.feature.student.command.model;

public class addstudentcommand :IRequest<Response<string>>
{
    
  
   //[Required (ErrorMessage = "required filed")]
    public string Name { get; set; }
   //[Required ]
    public string Address { get; set; }

    public string phone { get; set; }
    
    public int department_id { get; set; }
} 