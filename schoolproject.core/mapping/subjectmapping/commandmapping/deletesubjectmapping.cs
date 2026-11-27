using schoolproject.core.feature.subject.command.model;
using schoolproject.data.entites;

namespace schoolproject.core.mapping.subjectmapping
{
    public partial class subjectprofile
    {
        public void deletesubjectmapping()
        {
            CreateMap<deletesubjectcommand, Subjects>()
                .ForMember(dest => dest.SubID, opt => opt.MapFrom(src => src.Id));
        }
    }
}
