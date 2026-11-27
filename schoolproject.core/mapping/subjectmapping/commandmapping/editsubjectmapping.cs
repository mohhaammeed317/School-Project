using schoolproject.core.feature.subject.command.model;
using schoolproject.data.entites;

namespace schoolproject.core.mapping.subjectmapping
{
    public partial class subjectprofile
    {
        public void editsubjectmapping()
        {
            CreateMap<editsubject, Subjects>()
                .ForMember(dest => dest.SubID, opt => opt.MapFrom(src => src.SubID));
        }
    }
}
