using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using schoolproject.data.commons;

namespace schoolproject.data.entites
{
    public class Student : GeneralLocaliztionEntity
    {
        public Student()
        {
            StudentsSubjects = new HashSet<StudentSubject>();
        }
        [Key]
        public int StudID { get; set; }
        [StringLength(200)]
        public string Namear { get; set; }
        public string Nameen { get; set; }
        [StringLength(500)]
        public string Addressar { get; set; }
        public string Addressen { get; set; }
        [StringLength(500)]
        public string Phone { get; set; }
        public int? DID { get; set; }

        [ForeignKey("DID")]
        public virtual Department Department { get; set; }
        public virtual ICollection<StudentSubject> StudentsSubjects { get; set; }
    }
}
