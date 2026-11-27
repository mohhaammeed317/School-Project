using schoolproject.core.feature.subject.query.response;
using schoolproject.data.entites;

namespace schoolproject.core.mapping.subjectmapping
{
    public partial class subjectprofile
    {
        public void getsubjectbyidmapping()
        {
            CreateMap<Subjects, getsinglesubject>()
                .ForMember(dest => dest.Name,
                    opt => opt.MapFrom(src => src.Localize(src.SubjectNameen, src.SubjectNamear)));
        }
    }
}
