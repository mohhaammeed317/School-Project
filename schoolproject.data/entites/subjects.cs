using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using schoolproject.data.commons;

namespace schoolproject.data.entites
{
    public class Subjects : GeneralLocaliztionEntity
    {
        public Subjects()
        {
            StudentsSubjects = new HashSet<StudentSubject>();
            DepartmetsSubjects = new HashSet<DepartmetSubject>();
        }
        [Key]
        public int SubID { get; set; }
        [StringLength(500)]
        public string SubjectNamear { get; set; }
        public string SubjectNameen  { get; set; }
        public DateTime Period { get; set; }
        public virtual ICollection<StudentSubject> StudentsSubjects { get; set; }
        public virtual ICollection<DepartmetSubject> DepartmetsSubjects { get; set; }
    }
}
