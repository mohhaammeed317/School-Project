using schoolproject.data.entites;
using System;
using System.Collections.Generic;
using System.Text;



namespace schoolproject.infrastructure.abstracts

{
    public interface istudentrepository
{
        public Task<List<Student>> GetStudentsListAsync();
    }



}
