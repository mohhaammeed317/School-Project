using Microsoft.EntityFrameworkCore;
using schoolproject.data.entites;
using schoolproject.infrastructure.abstracts;
using schoolproject.infrastructure.data;
using schoolproject.infrastructure.infrabases;

namespace schoolproject.infrastructure.repositories
{
    public class subjectrepository : genericrepo<Subjects>, isubjectrepository
    {
        private readonly DbSet<Subjects> _subjects;

        public subjectrepository(applicationdbcontext dBContext) : base(dBContext)
        {
            _subjects = dBContext.Set<Subjects>();
        }

        public async Task<List<Subjects>> GetSubjectsListAsync()
        {
            return await _subjects.ToListAsync();
        }

        public async Task<Subjects> GetSubjectbyidtAsync(int id)
        {
            return await _subjects
                .Include(x => x.StudentsSubjects)
                .Include(x => x.DepartmetsSubjects)
                .FirstOrDefaultAsync(x => x.SubID == id);
        }
    }
}
