using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeTracking.Models
{
    public class TableRecordModel
    {
        public Int32 DayNumber { get; set; }
        public Double Hours { get; set; }
        public Boolean IsDayoff { get; set; }
    }
}