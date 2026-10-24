using MediatR;
using schoolproject.data.entites;
using System;
using System.Collections.Generic;
using System.Text;
using schoolproject.core.bases;

namespace schoolproject.core.feature.student.query.model
{
    public class getstudentbyidquery : IRequest<Response<Student>>
    {
       public int id { get; set; }

        public getstudentbyidquery(int id)
        {
            this.id = id;
        }
    }
}
