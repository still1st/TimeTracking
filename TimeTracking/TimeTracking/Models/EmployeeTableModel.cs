using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeTracking.Models
{
    public class EmployeeTableModel
    {
        public Int64 EmployeeId { get; set; }
        public String EmployeeName { get; set; }
        public Double Plan { get; set; }
        public Double Fact { get; set; }
        public IEnumerable<TableRecordModel> Records { get; set; }
    }
}