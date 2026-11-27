using Microsoft.EntityFrameworkCore;
using schoolproject.data.entites;
using schoolproject.infrastructure.abstracts;
using schoolproject.infrastructure.data;
using schoolproject.infrastructure.infrabases;

namespace schoolproject.infrastructure.repositories
{
    public class departmentrepository : genericrepo<Department>, idepartmentrepository
    {
        private readonly DbSet<Department> _departments;

        public departmentrepository(applicationdbcontext dBContext) : base(dBContext)
        {
            _departments = dBContext.Set<Department>();
        }

        public async Task<List<Department>> GetDepartmentsListAsync()
        {
            return await _departments.ToListAsync();
        }

        public async Task<Department> GetDepartmentbyidtAsync(int id)
        {
            return await _departments
                .Include(x => x.Students)
                .Include(x => x.DepartmentSubjects)
                .FirstOrDefaultAsync(x => x.DID == id);
        }
    }
}
