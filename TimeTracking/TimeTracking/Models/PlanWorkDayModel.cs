using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeTracking.Models
{
    public class PlanWorkDayModel
    {
        public Int64 PlanWorkDayId { get; set; }
        public Int32 GroupId { get; set; }
        public String Group { get; set; }
        public Double Hours { get; set; }
    }
}