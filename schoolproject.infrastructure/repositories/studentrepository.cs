using Microsoft.EntityFrameworkCore;
using schoolproject.data.entites;
using schoolproject.infrastructure.abstracts;
using schoolproject.infrastructure.data;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;


namespace schoolproject.infrastructure.repositories
{
    public class studentrepository : istudentrepository
    {
        private readonly applicationdbcontext _dBContext;


        public studentrepository(applicationdbcontext dBContext)
        {
            _dBContext = dBContext;
        }

        public async Task<List<Student>> GetStudentbyarrAsync()
        {
            //here he will include department because we want to get the department name for each student

            return await _dBContext.students.Include(s => s.Department).ToListAsync();

        }

        public async Task<Student> GetStudentbyidtAsync(int id)
        {
            return await _dBContext.students.FirstOrDefaultAsync(s => s.StudID == id);

        }

        public async Task<List<Student>> GetStudentsListAsync()
        {
           return await _dBContext.students.ToListAsync();
        }
    }
}
