using AutoMapper;

namespace schoolproject.core.mapping.departmentmapping
{
    public partial class departmentprofile : Profile
    {
        public departmentprofile()
        {
            adddepartmentmapping();
            editdepartmentmapping();
            deletedepartmentmapping();
            getdepartmentbyidmapping();
        }
    }
}
