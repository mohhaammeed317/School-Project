using AutoMapper;
using MediatR;
using schoolproject.core.bases;
using schoolproject.core.feature.student.command.model;
using schoolproject.data.entites;
using schoolproject.service.abstracts;

namespace schoolproject.core.feature.student.command.handel;

public class studentcommandhandler : ResponseHandler , IRequestHandler<addstudentcommand,Response<string>>
{
    
    private readonly istudentservice _studentService;
    private readonly IMapper _mapper;
    public studentcommandhandler(istudentservice studentService, IMapper mapper)
    {
        _studentService = studentService;
        _mapper = mapper;
    }
    
    
    
    public async Task<Response<string>> Handle(addstudentcommand request, CancellationToken cancellationToken)
    {
        // mapping between request and student 
        var stdlistmapper = _mapper.Map<Student>(request);
        // add the student 
        var result =await _studentService.addsync(stdlistmapper);
        // check condition 
        if (result == "name is alerady exisit") return UnprocessableEntity<string>("name is alerady exist");
        else if (result=="added successrfully") return Success("added successrfully");
        else
        {
            return BadRequest<string>("something went wronge");
        }
 

        
        //   throw new NotImplementedException();
    }
}