using AutoMapper;
using PixelStamp.Core.Dtos;
using PixelStamp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PixelStamp.Core.Mapping
{
    public class CourseMappingProfile : Profile
    {
        public CourseMappingProfile()
        {
            #region Course
            CreateMap<Course, CourseDto>();

            CreateMap<CourseDto, Course>()
                .ForMember(res => res.Lessons, opt => opt.Ignore());
            #endregion
        }
    }
}
