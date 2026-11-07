using schoolproject.core.feature.student.command.model;
using schoolproject.core.feature.student.query.response;
using schoolproject.data.entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace schoolproject.core.mapping.studentmapping
{

    public partial class studentprofile
    {


        public void addstdmapping()
        {
            //when he excute the fun of handler when he read this line( var stdlistmapper = _mapper.Map<List<getspecificattributefromstd>>(stdlist);)
            //he comes her
            //create map between <source,des>
            //sorce here is name of entity
            //for member is used to map the attribute name  in des to a specific attribute name in source

            CreateMap<addstudentcommand, Student>()
                .ForMember(dest => dest.DID, opt => opt.MapFrom(src => src.department_id));
        }

    }
}