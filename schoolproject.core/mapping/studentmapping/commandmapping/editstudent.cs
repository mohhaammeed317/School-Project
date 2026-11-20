using schoolproject.core.feature.student.command.model;
using schoolproject.data.entites;

namespace schoolproject.core.mapping.studentmapping;

public partial class studentprofile
{
    public void editstudentmapping()
    {
        CreateMap<editstudent, Student>()
            .ForMember(dest => dest.DID, opt => opt.MapFrom(src => src.department_id)).
        ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.phone_number)).
            ForMember(dest => dest.StudID, opt => opt.MapFrom(src => src.id));
        // if you want to ignore the id 
        //.formember(dest=>dest.studid,opt.ignore());
    }
   
    
}