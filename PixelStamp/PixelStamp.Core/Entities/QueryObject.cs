using System;

namespace PixelStamp.Core.Entities
{
    public class QueryObject
    {
        public string SortBy { get; set; }

        public bool IsSortAscending { get; set; }

        public int Page { get; set; }

        public int PageSize { get; set; }
    }

    public class UserQueryObject : QueryObject
    {
        public bool Enabled { get; set; } = false;
        public string Roles { get; set; }
        public int? BranchId { get; set; }
        public int? DepartmetId { get; set; }
        public int? WorkItemTypeId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string IdentityNumber { get; set; }
        public string SearchText { get; set; }

        public bool? HasConfidentialPermission { get; set; }
    }


}