using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using schoolproject.data.entites;
using schoolproject.data.helpers;
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
            var std = await _studentrepository.GetTableNoTracking().Where(x => x.Name.Equals(name)&!x.StudID.Equals(id)).FirstOrDefaultAsync();
            if (std == null)
                return true;
            return false;  
        }

        public async Task<bool> checkondidusingid(int id)
        {
            var std =  await _studentrepository.GetTableNoTracking().Where(x => x.StudID.Equals(id)&x.DID.Equals(null)).FirstOrDefaultAsync();
            if (std != null)
                return true;
            return false;

        }

        public async Task<string> deletestudent(Student  student)
        {
            var trans = _studentrepository.BeginTransaction();
            try
            {
                await _studentrepository.DeleteAsync(student);
                await trans.CommitAsync();
                return "deleted successrfully";

            }
            catch
            {
                 await trans.RollbackAsync();
                return "failed ";
            }
        }

        public Task<Student> GetStudentbyidAsync(int id)
        {
            var std = _studentrepository.GetByIdAsync(id);
            return std;
        }

        public IQueryable<Student> getstudentquerable()
        {
            var res = _studentrepository.GetTableNoTracking().Include(x=>x.Department).AsQueryable();
            return res;
        }

        public IQueryable<Student> filterstudentpaginatedquerable(studentorderingenum order, string search)
        {
          //  var ord;
            var res = _studentrepository.GetTableNoTracking().Include(x=>x.Department).AsQueryable();
            if (search != null)
            {
                var ser= res.Where(x => x.Name.Contains(search) || x.Address.Contains(search));
                return ser;  
            }

            switch (order)
            {
                case studentorderingenum.StudID:
                  res = res.OrderBy(x => x.StudID);
                    break;
                case studentorderingenum.Name:
                 res = res.OrderBy(x => x.Name);   
                    break;
                case studentorderingenum.Address:
                  res = res.OrderBy(x => x.Address); 
                    break;
                case studentorderingenum.DepartmentName:
                   res = res.OrderBy(x => x.Department.DName); 
                    break;
                default:
                  res = res.OrderBy(x => x.StudID);
                    break;
            }
         return res; 
          
        } 

        public async Task<Student> GetStudentbyidwithincludetAsync(int id)
        {
            return await _studentrepository.GetStudentbyidtAsync(id);
        }

        public async Task<List<Student>> GetStudentsListAsync()
        {
            return await _studentrepository.GetStudentsListAsync();
        }
    }
}
