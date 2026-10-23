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
        public async Task<List<Student>> GetStudentsListAsync()
        {
            return await _studentrepository.GetStudentsListAsync();
        }
    }
}
