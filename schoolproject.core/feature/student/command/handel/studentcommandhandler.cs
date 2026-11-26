using AutoMapper;
using MediatR;
using schoolproject.core.bases;
using schoolproject.core.feature.student.command.model;
using schoolproject.data.entites;
using schoolproject.service.abstracts;

namespace schoolproject.core.feature.student.command.handel;

public class studentcommandhandler : ResponseHandler , IRequestHandler<addstudentcommand,Response<string>> , IRequestHandler<editstudent,Response<string>>,IRequestHandler<deletcommand,Response<string>>
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
         if (result=="added successrfully") return Success("added successrfully");
        else
        {
            return BadRequest<string>("something went wronge");
        }

    }

    public async Task<Response<string>> Handle(editstudent request, CancellationToken cancellationToken)
    {
        // check if id is exist or not  
        var std= await _studentService.GetStudentbyidAsync(request.id);
       if (std == null)
        {
            return NotFound<string>("student not found");
        }
          
       var edit_std_mapp = _mapper.Map(request, std);
       // var  edit_std_mapp  = _mapper.Map<std>(request);
       var result = await _studentService.updatestudent(edit_std_mapp);
       if (result == "updated successrfully")
       {
           return Success($"updated successrfully{edit_std_mapp.StudID}");
       }
       else
       {
           return BadRequest<string>("something went wrong");
       }

    }

    public async Task<Response<string>> Handle(deletcommand request, CancellationToken cancellationToken)
    {
        var std= await _studentService.GetStudentbyidAsync(request.Id);
        if (std == null)
        {
            return NotFound<string>("student not found");
        }

        var result =await _studentService.deletestudent(std);
        if (result == "deleted successrfully")
            return Deleted<String>($"deleted successrfully{std.StudID}");

          return  BadRequest<string>("failed");
        

  

    }
}