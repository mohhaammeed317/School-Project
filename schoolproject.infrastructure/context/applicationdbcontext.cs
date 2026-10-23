using Microsoft.EntityFrameworkCore;
using schoolproject.data.entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace schoolproject.infrastructure.data
{
    public class applicationdbcontext : DbContext
    {
        public applicationdbcontext()
        {


        }
        public applicationdbcontext(DbContextOptions<applicationdbcontext> options) : base(options)
        {
           // _encryptionProvider = new GenerateEncryptionProvider("8a4dcaaec64d412380fe4b02193cd26f");
        }
        //public DbSet<User> User { get; set; }
        public DbSet<Department> departments { get; set; }
        public DbSet<Student> students { get; set; }
        public DbSet<DepartmetSubject> departmetSubjects { get; set; }
        public DbSet<Subjects> subjects { get; set; }
        public DbSet<StudentSubject> studentSubjects { get; set; }



    }

    }
