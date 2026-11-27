using schoolproject.data.entites;
using schoolproject.data.helpers;

namespace schoolproject.service.abstracts
{
    public interface idepartmentservice
    {
        Task<List<Department>> GetDepartmentsListAsync();
        Task<Department> GetDepartmentbyidAsync(int id);
        Task<Department> GetDepartmentbyidwithincludetAsync(int id);
        Task<string> addsync(Department department);
        Task<string> updatedepartment(Department department);
        Task<string> deletedepartment(Department department);
        Task<bool> nameexist(string name);
        Task<bool> isnameisexistexcludeself(string name, int id);
        Task<bool> candelete(int id);
        IQueryable<Department> getdepartmentquerable();
        IQueryable<Department> filterdepartmentpaginatedquerable(departmentorderingenum order, string search);
    }
}
