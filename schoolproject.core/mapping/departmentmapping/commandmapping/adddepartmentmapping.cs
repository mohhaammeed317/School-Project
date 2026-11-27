using schoolproject.core.feature.department.command.model;
using schoolproject.data.entites;

namespace schoolproject.core.mapping.departmentmapping
{
    public partial class departmentprofile
    {
        public void adddepartmentmapping()
        {
            CreateMap<adddepartmentcommand, Department>();
        }
    }
}
