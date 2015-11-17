using System;
using System.Collections.Generic;

namespace TimeTracking.Domain.Models
{
    public class Table
    {
        public virtual Int64 TableId { get; set; }

        public virtual Int32 Month { get; set; }

        public virtual Int32 Year { get; set; }

        public virtual ICollection<TableRecord> Records { get; set; }
    }
}
