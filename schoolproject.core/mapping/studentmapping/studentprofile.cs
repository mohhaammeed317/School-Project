using AutoMapper;
using schoolproject.core.feature.student.query.response;
using schoolproject.data.entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace schoolproject.core.mapping.studentmapping
{
    public partial class studentprofile: Profile 
    {

        //partial classes must  have the same name and have same name spaces 
        public studentprofile()
        {

            stdmapping();
            getstdbyidmapping();

        }


    }
}
    