using System;
using System.Collections.Generic;
using System.Text;

namespace schoolproject.data.appmetadata
{
    public static class router
    {
        // [Route("api/[controller]")]
        public const string singleroute =  "/{id}";
        public const string root = "api";
        public const string version = "v1";
        public const string rule = root + "/" + version + "/";
      
        public static class studentrouting
        {
            public const string prefix = rule + "student";
            public const string getstudentlist = prefix +"/"+ "list";
            public const string getstudentbyid = prefix + "/" + "getbyid" + singleroute;
            public const string getstudentbyarr = prefix + "/" + "listwithanotherarrtribute";
            public const string addstd = prefix + "/" + "addstd";
            public const string editstd = prefix + "/" + "editstd";
            public const string deletestd = prefix + "/" + "deletestd"+singleroute;
            public const string getstudentpaginited  = prefix + "/" + "getstudentpaginited";


        } 

        public static class departmentrouting
        {
            public const string prefix = rule + "department";
            public const string getdepartmentlist = prefix + "/" + "list";
            public const string getdepartmentbyid = prefix + "/" + "getbyid" + singleroute;
            public const string adddepartment = prefix + "/" + "adddepartment";
            public const string editdepartment = prefix + "/" + "editdepartment";
            public const string deletedepartment = prefix + "/" + "deletedepartment" + singleroute;
            public const string getdepartmentpaginited = prefix + "/" + "getdepartmentpaginited";
        }

        public static class subjectrouting
        {
            public const string prefix = rule + "subject";
            public const string getsubjectlist = prefix + "/" + "list";
            public const string getsubjectbyid = prefix + "/" + "getbyid" + singleroute;
            public const string addsubject = prefix + "/" + "addsubject";
            public const string editsubject = prefix + "/" + "editsubject";
            public const string deletesubject = prefix + "/" + "deletesubject" + singleroute;
            public const string getsubjectpaginited = prefix + "/" + "getsubjectpaginited";
        }
        
        
    }
}
