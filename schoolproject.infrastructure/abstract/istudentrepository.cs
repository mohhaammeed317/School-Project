using schoolproject.data.entites;
using schoolproject.infrastructure.infrabases;
using System;
using System.Collections.Generic;
using System.Text;



namespace schoolproject.infrastructure.abstracts

{
    public interface istudentrepository : igenericrepo<Student>
{
        public Task<List<Student>> GetStudentsListAsync();

        public Task<Student> GetStudentbyidtAsync(int id);
        public Task<List<Student>> GetStudentbyarrAsync();



    }



}
