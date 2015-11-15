using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeTracking.Models
{
    public class PlanWorkMonthModel
    {
        public String Month { get; set; }
        public List<GroupModel> Groups { get; set; }

        public PlanWorkMonthModel()
        {
            Groups = new List<GroupModel>();
        }
    }

    public class GroupModel
    {
        public String Group { get; set; }
        public Double Hours { get; set; }
    }
}