using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracking.Domain.Models;

namespace TimeTracking.Services
{
    public interface ITableService
    {
        /// <summary>
        /// Calcs work time for the employee for the month and year
        /// </summary>
        /// <param name="year">Year</param>
        /// <param name="month">Month</param>
        /// <param name="employee">Employee</param>
        /// <returns>Collection of the table records</returns>
        IEnumerable<TableRecord> CalcMonth(Int32 year, Int32 month, Employee employee);
    }
}
