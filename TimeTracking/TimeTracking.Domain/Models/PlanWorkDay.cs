using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracking.Domain.Enums;

namespace TimeTracking.Domain.Models
{
    public class PlanWorkDay
    {
        /// <summary>
        /// Gets or sets the plan worktime id
        /// </summary>
        public virtual Int64 PlanWorkDayId { get; set; }

        /// <summary>
        /// Gets or sets the employee group
        /// </summary>
        public virtual EmployeeGroup EmployeeGroup { get; set; }

        /// <summary>
        /// Gets or sets the standart count of hours in work day
        /// </summary>
        public virtual Double Hours { get; set; }
    }
}
