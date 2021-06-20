using Microsoft.AspNetCore.Identity;
using PixelStamp.Core.Dtos;
using System;
using System.Collections.Generic;

namespace PixelStamp.Core.Entities
{
    public class Question 
    {
        public int Id { get; set; }
        public string Text { get; set; }
    }
}
