using Microsoft.EntityFrameworkCore;
using schoolproject.data.entites;
using schoolproject.data.helpers;
using schoolproject.infrastructure.abstracts;
using schoolproject.service.abstracts;

namespace schoolproject.service.immplemintaion
{
    public class departmentservice : idepartmentservice
    {
        private readonly idepartmentrepository _departmentrepository;

        public departmentservice(idepartmentrepository departmentrepository)
        {
            _departmentrepository = departmentrepository;
        }

        public async Task<List<Department>> GetDepartmentsListAsync()
        {
            return await _departmentrepository.GetDepartmentsListAsync();
        }

        public Task<Department> GetDepartmentbyidAsync(int id)
        {
            return _departmentrepository.GetByIdAsync(id);
        }

        public async Task<Department> GetDepartmentbyidwithincludetAsync(int id)
        {
            return await _departmentrepository.GetDepartmentbyidtAsync(id);
        }

        public async Task<string> addsync(Department department)
        {
            await _departmentrepository.AddAsync(department);
            return "added successrfully";
        }

        public async Task<string> updatedepartment(Department department)
        {
            await _departmentrepository.UpdateAsync(department);
            return "updated successrfully";
        }

        public async Task<string> deletedepartment(Department department)
        {
            var trans = _departmentrepository.BeginTransaction();
            try
            {
                await _departmentrepository.DeleteAsync(department);
                await trans.CommitAsync();
                return "deleted successrfully";
            }
            catch
            {
                await trans.RollbackAsync();
                return "failed ";
            }
        }

        // Mirrors studentservice.nameexist: true means the name is free to use.
        public async Task<bool> nameexist(string name)
        {
            var department = await _departmentrepository.GetTableNoTracking()
                .FirstOrDefaultAsync(x => x.DNameen == name || x.DNamear == name);
            return department == null;
        }

        // Mirrors studentservice.isnameisexistexcludeself: true means the name is free to use.
        public async Task<bool> isnameisexistexcludeself(string name, int id)
        {
            var department = await _departmentrepository.GetTableNoTracking()
                .FirstOrDefaultAsync(x => (x.DNameen == name || x.DNamear == name) && x.DID != id);
            return department == null;
        }

        public async Task<bool> candelete(int id)
        {
            var isUsed = await _departmentrepository.GetTableNoTracking()
                .AnyAsync(x => x.DID == id && (x.Students.Any() || x.DepartmentSubjects.Any()));
            return !isUsed;
        }

        public IQueryable<Department> getdepartmentquerable()
        {
            return _departmentrepository.GetTableNoTracking()
                .Include(x => x.Students)
                .Include(x => x.DepartmentSubjects)
                .AsQueryable();
        }

        public IQueryable<Department> filterdepartmentpaginatedquerable(departmentorderingenum order, string search)
        {
            var res = _departmentrepository.GetTableNoTracking().AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                res = res.Where(x => x.DNameen.Contains(search) || x.DNamear.Contains(search));
            }

            IOrderedQueryable<Department> ordered = order switch
            {
                departmentorderingenum.Name => res.OrderBy(x => x.DNameen),
                _ => res.OrderBy(x => x.DID)
            };

            return ordered.ThenBy(x => x.DID);
        }
    }
}
