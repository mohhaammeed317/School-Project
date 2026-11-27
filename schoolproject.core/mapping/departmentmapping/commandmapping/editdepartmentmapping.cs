using schoolproject.core.feature.department.command.model;
using schoolproject.data.entites;

namespace schoolproject.core.mapping.departmentmapping
{
    public partial class departmentprofile
    {
        public void editdepartmentmapping()
        {
            CreateMap<editdepartment, Department>()
                .ForMember(dest => dest.DID, opt => opt.MapFrom(src => src.DID));
        }
    }
}
