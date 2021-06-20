using System.Collections.Generic;

namespace PixelStamp.Core.Dtos
{
    public class QueryResultDto<T>
    {
        public int TotalItems { get; set; }

        public IEnumerable<T> Items { get; set; }
    }
}
