using schoolproject.data.entites;
using schoolproject.infrastructure.infrabases;

namespace schoolproject.infrastructure.abstracts
{
    public interface idepartmentrepository : igenericrepo<Department>
    {
        Task<List<Department>> GetDepartmentsListAsync();
        Task<Department> GetDepartmentbyidtAsync(int id);
    }
}
