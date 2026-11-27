using AutoMapper;

namespace schoolproject.core.mapping.subjectmapping
{
    public partial class subjectprofile : Profile
    {
        public subjectprofile()
        {
            addsubjectmapping();
            editsubjectmapping();
            deletesubjectmapping();
            getsubjectbyidmapping();
        }
    }
}
