using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeTracking.Models
{
    public class TableInfoModel
    {
        public Int64 TableId { get; set; }
        public Int32 Year { get; set; }
        public String Month { get; set; }
    }
}