using schoolproject.data.entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace schoolproject.service.abstracts
{
    public interface istudentservice
    {

        public Task<List<Student>> GetStudentsListAsync();
        public Task<Student> GetStudentbyidtAsync(int id);
        public Task<List<Student>> GetStudentbyarrAsync();
        public Task<string> addsync(Student student);
        public Task<bool> nameexist(string name);
        public Task<string> updatestudent(Student student);
        public Task<bool> isnameisexistexcludeself(string name,int id);
        


    }
}
