using System;
using System.Collections.Generic;
using TimeTracking.Domain.Models;

namespace TimeTracking.Services.Reports
{
    public class Report
    {
        public Int32 Year { get; set; }
        public Int32 Month { get; set; }

        /// <summary>
        /// Collection of employees lines
        /// </summary>
        public List<EmployeeLine> EmployeeLines { get; private set; }

        /// <summary>
        /// Initializes a new instance of <see cref="Report"/>
        /// </summary>
        public Report()
        {
            EmployeeLines = new List<EmployeeLine>();
        }

        #region nested classes
        public class EmployeeLine
        {
            public String FullName { get; set; }
            public String Post { get; set; }

            public WeekLine FirstWeek { get; private set; }
            public WeekLine SecondWeek { get; private set; }

            public Int32 TotalAppeareance { get; set; }
            public Double TotalHours { get; set; }

            public Int32 TotalVacation { get; set; }
            public Int32 TotalWithoutContent { get; set; }

            public EmployeeLine()
            {
                FirstWeek = new WeekLine();
                SecondWeek = new WeekLine();
            }
        }

        public class WeekLine
        {
            public IEnumerable<TableRecord> Records { get; set; }

            public Int32 TotalAppereance { get; set; }
            public Double TotalHours { get; set; }
        } 
        #endregion
    }
}
