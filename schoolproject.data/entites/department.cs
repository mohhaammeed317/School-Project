using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using schoolproject.data.commons;

namespace schoolproject.data.entites
{
    public partial class Department : GeneralLocaliztionEntity
    {
        public Department()
        {
            Students = new HashSet<Student>();
            DepartmentSubjects = new HashSet<DepartmetSubject>();
        }
        [Key]
        public int DID { get; set; }
        [StringLength(500)]
        public string DNamear { get; set; } 
        public string DNameen { get; set; } 
        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<DepartmetSubject> DepartmentSubjects { get; set; }
    }
}
