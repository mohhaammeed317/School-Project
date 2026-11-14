using System;
using System.Collections.Generic;
using System.Text;
using schoolproject.data.entites;
using schoolproject.infrastructure.abstracts;
using schoolproject.service.abstracts;

namespace schoolproject.service.immplemintaion
{
    public class studentservice : istudentservice
    {
       
        private readonly istudentrepository _studentrepository;

        public studentservice(istudentrepository studentrepository)
        {
            
           _studentrepository = studentrepository;
        }

        public async Task<List<Student>> GetStudentbyarrAsync()
        {
            return await _studentrepository.GetStudentbyarrAsync();
        }

        public async Task<string> addsync(Student student)
        {
          //check if the name is exist in data base or nor
         /* var std = _studentrepository.GetTableNoTracking().Where(x => x.Name.Equals(student.Name)).FirstOrDefault();
          if (std != null)
              return "name is alerady exisit";
          else*/
            
              await  _studentrepository.AddAsync(student);
          return "added successrfully";


        }

        public async Task<bool> nameexist(string name)
        {
            //check if the name is exist in data base or nor
            var std = _studentrepository.GetTableNoTracking().Where(x => x.Name.Equals(name)).FirstOrDefault();
            if (std == null)
                return true;
            return false;    
        }

        public async Task<string> updatestudent(Student student)
        {
             await _studentrepository.UpdateAsync(student);
             return "updated successrfully";
        }

        public async Task<bool> isnameisexistexcludeself(string name, int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Student> GetStudentbyidtAsync(int id)
        {
            return await _studentrepository.GetStudentbyidtAsync(id);
        }

        public async Task<List<Student>> GetStudentsListAsync()
        {
            return await _studentrepository.GetStudentsListAsync();
        }
    }
}
