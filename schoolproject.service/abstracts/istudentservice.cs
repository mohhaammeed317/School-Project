using schoolproject.data.entites;
using System;
using System.Collections.Generic;
using System.Text;
using schoolproject.data.helpers;

namespace schoolproject.service.abstracts
{
    public interface istudentservice
    {

        public Task<List<Student>> GetStudentsListAsync();
        public Task<Student> GetStudentbyidwithincludetAsync(int id);
        public Task<List<Student>> GetStudentbyarrAsync();
        public Task<string> addsync(Student student);
        public Task<bool> nameexist(string name);
        public Task<string> updatestudent(Student student);
        public Task<bool> isnameisexistexcludeself(string name,int id);
        public Task<bool> checkondidusingid(int id);
        public Task<string> deletestudent(Student  student);
        public Task<Student> GetStudentbyidAsync(int id);
        public IQueryable<Student> getstudentquerable();
        public IQueryable<Student> filterstudentpaginatedquerable(studentorderingenum order, string search);

        


    }
}
