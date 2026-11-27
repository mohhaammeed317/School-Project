using schoolproject.core.feature.department.query.response;
using schoolproject.data.entites;

namespace schoolproject.core.mapping.departmentmapping
{
    public partial class departmentprofile
    {
        public void getdepartmentbyidmapping()
        {
            CreateMap<Department, getsingledepartment>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Localize(src.DNameen, src.DNamear)));
        }
    }
}
