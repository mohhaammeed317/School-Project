using MediatR;
using schoolproject.core.bases;
using schoolproject.core.feature.student.query.response;
using schoolproject.data.entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace schoolproject.core.feature.student.query.model
{
    public class getstudentbyidquery : IRequest<Response<getsinglestudent>>
    {


        //any thing you want from user defined in model 
       public int id { get; set; }

        public getstudentbyidquery(int id)
        {
            this.id = id;
        }
    }
}
