using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeTracking.Models
{
    public class TableModel
    {
        public Int64 TableId { get; set; }
        public Int32 Month { get; set; }
        public Int32 Year { get; set; }
        public IEnumerable<EmployeeTableModel> Employees { get; set; }
    }
}