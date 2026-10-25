using System;
using System.Collections.Generic;
using System.Text;

namespace schoolproject.data.appmetadata
{
    public static class router
    {
          public const string singleroute =  "/{id}";
        public const string root = "api";
        public const string version = "v1";
        public const string rule = root + "/" + version + "/";
      
        public static class studentrouting
        {
            public const string prefix = rule + "student";
            public const string getstudentlist = prefix +"/"+ "list";
            public const string getstudentbyid = prefix + "/" + "getbyid" + singleroute;
            public const string getstudentbyarr = prefix + "/" + "listmap";


        }
    }
}
