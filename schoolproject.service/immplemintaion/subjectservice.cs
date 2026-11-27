using Microsoft.EntityFrameworkCore;
using schoolproject.data.entites;
using schoolproject.data.helpers;
using schoolproject.infrastructure.abstracts;
using schoolproject.service.abstracts;

namespace schoolproject.service.immplemintaion
{
    public class subjectservice : isubjectservice
    {
        private readonly isubjectrepository _subjectrepository;

        public subjectservice(isubjectrepository subjectrepository)
        {
            _subjectrepository = subjectrepository;
        }

        public async Task<List<Subjects>> GetSubjectsListAsync()
        {
            return await _subjectrepository.GetSubjectsListAsync();
        }

        public Task<Subjects> GetSubjectbyidAsync(int id)
        {
            return _subjectrepository.GetByIdAsync(id);
        }

        public async Task<Subjects> GetSubjectbyidwithincludetAsync(int id)
        {
            return await _subjectrepository.GetSubjectbyidtAsync(id);
        }

        public async Task<string> addsync(Subjects subject)
        {
            await _subjectrepository.AddAsync(subject);
            return "added successrfully";
        }

        public async Task<string> updatesubject(Subjects subject)
        {
            await _subjectrepository.UpdateAsync(subject);
            return "updated successrfully";
        }

        public async Task<string> deletesubject(Subjects subject)
        {
            var trans = _subjectrepository.BeginTransaction();
            try
            {
                await _subjectrepository.DeleteAsync(subject);
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
            var subject = await _subjectrepository.GetTableNoTracking()
                .FirstOrDefaultAsync(x => x.SubjectNameen == name || x.SubjectNamear == name);
            return subject == null;
        }

        // Mirrors studentservice.isnameisexistexcludeself: true means the name is free to use.
        public async Task<bool> isnameisexistexcludeself(string name, int id)
        {
            var subject = await _subjectrepository.GetTableNoTracking()
                .FirstOrDefaultAsync(x => (x.SubjectNameen == name || x.SubjectNamear == name) && x.SubID != id);
            return subject == null;
        }

        public async Task<bool> candelete(int id)
        {
            var isUsed = await _subjectrepository.GetTableNoTracking()
                .AnyAsync(x => x.SubID == id && (x.StudentsSubjects.Any() || x.DepartmetsSubjects.Any()));
            return !isUsed;
        }

        public IQueryable<Subjects> getsubjectquerable()
        {
            return _subjectrepository.GetTableNoTracking()
                .Include(x => x.StudentsSubjects)
                .Include(x => x.DepartmetsSubjects)
                .AsQueryable();
        }

        public IQueryable<Subjects> filtersubjectpaginatedquerable(subjectorderingenum order, string search)
        {
            var res = _subjectrepository.GetTableNoTracking().AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                res = res.Where(x => x.SubjectNameen.Contains(search) || x.SubjectNamear.Contains(search));
            }

            IOrderedQueryable<Subjects> ordered = order switch
            {
                subjectorderingenum.Name => res.OrderBy(x => x.SubjectNameen),
                subjectorderingenum.Period => res.OrderBy(x => x.Period),
                _ => res.OrderBy(x => x.SubID)
            };

            return ordered.ThenBy(x => x.SubID);
        }
    }
}
