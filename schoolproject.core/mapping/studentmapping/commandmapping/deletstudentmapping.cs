using schoolproject.core.feature.student.command.model;
using schoolproject.data.entites;

namespace schoolproject.core.mapping.studentmapping;

public partial class studentprofile
{
    public void deleteStudentMapping()
    {
        CreateMap<deletcommand, Student>()
            .ForMember(dest => dest.DID, opt => opt.MapFrom(src => src.Id)); 
        
    }

}