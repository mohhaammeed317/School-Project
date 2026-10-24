using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace schoolproject.core.feature.student.query.response
{
    public class getspecificattributefromstd 
    {

        public int StudID { get; set; }
      
        public string? Name { get; set; }
       
        public string? Address { get; set; }
  
        public string? DepartmentName { get; set; }

    }
}
