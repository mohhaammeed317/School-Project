using schoolproject.core.feature.subject.command.model;
using schoolproject.data.entites;

namespace schoolproject.core.mapping.subjectmapping
{
    public partial class subjectprofile
    {
        public void addsubjectmapping()
        {
            CreateMap<addsubjectcommand, Subjects>();
        }
    }
}
