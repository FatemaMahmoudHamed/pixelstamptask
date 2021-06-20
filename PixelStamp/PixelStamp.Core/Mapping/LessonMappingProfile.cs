using AutoMapper;
using PixelStamp.Core.Dtos;
using PixelStamp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PixelStamp.Core.Mapping
{
    public class LessonMappingProfile : Profile
    {
        public LessonMappingProfile()
        {
            #region Lesson
            CreateMap<LessonDto, Lesson>().ReverseMap();
            #endregion
        }
    }
}
