using Microsoft.EntityFrameworkCore;
using schoolproject.data.entites;
using schoolproject.infrastructure.abstracts;
using schoolproject.infrastructure.data;
using schoolproject.infrastructure.infrabases;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;


namespace schoolproject.infrastructure.repositories
{
    public class studentrepository : genericrepo<Student>,istudentrepository
    {
       //private readonly applicationdbcontext _dBContext;

        private readonly  DbSet<Student> _students;

        public studentrepository(applicationdbcontext dBContext): base(dBContext)
        {
            //dBContext = dBContext;
            _students = dBContext.Set<Student>();
        }

        public async Task<List<Student>> GetStudentbyarrAsync()
        {
            //here he will include department because we want to get the department name for each student

            return await _students.Include(s => s.Department).ToListAsync();

        }

        public async Task<Student> GetStudentbyidtAsync(int id)
        {
            return await _students.Include(x => x.Department).FirstOrDefaultAsync(s => s.StudID == id);

        }

        public async Task<List<Student>> GetStudentsListAsync()
        {
           return await _students.ToListAsync();
        }
    }
}
