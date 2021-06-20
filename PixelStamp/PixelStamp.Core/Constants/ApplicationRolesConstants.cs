using System;

namespace PixelStamp.Core.Constants
{
    public static class ApplicationRolesConstants
    {
        /// <summary>
        /// Teacher
        /// </summary>
        public static class Teacher
        {
            public static readonly Guid Code = new Guid("be931c3d-31d3-481e-8ba6-3dacd3513c56");
            public static readonly string Name = "Teacher";
        }

        /// <summary>
        /// Student
        /// </summary>
        public static class Student
        {
            public static readonly Guid Code = new Guid("ef5e1433-1bd6-4b3a-b424-0c6b0acb1b07");
            public static readonly string Name = "Student";
        }
    }
}
