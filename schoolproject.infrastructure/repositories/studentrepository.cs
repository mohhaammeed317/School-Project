using Microsoft.EntityFrameworkCore;
using schoolproject.data.entites;
using schoolproject.infrastructure.abstracts;
using schoolproject.infrastructure.data;
using System;
using System.Collections.Generic;
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



        public async Task<List<Student>> GetStudentsListAsync()
        {
           return await _dBContext.students.ToListAsync();
        }
    }
}
