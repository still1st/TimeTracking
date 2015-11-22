using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracking.Domain.Enums;
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

        /// <summary>
        /// Calc work time for the month and for the planWorkday
        /// </summary>
        /// <param name="year">Year</param>
        /// <param name="month">Month</param>
        /// <param name="planWorkday">Plan workday</param>
        /// <returns>PlanWorkmonth</returns>
        Double CalcMonth(Int32 year, Int32 month, Double planWorkday);

        /// <summary>
        /// Calc work time for the month and for the employee group
        /// </summary>
        /// <param name="year">Year</param>
        /// <param name="month">Month</param>
        /// <param name="planWorkday">Employee group</param>
        /// <returns>PlanWorkmonth</returns>
        Double CalcMonth(Int32 year, Int32 month, EmployeeGroup group);

        /// <summary>
        /// Adds a new table
        /// </summary>
        /// <param name="table">Table entity</param>
        void AddTable(Table table);

        /// <summary>
        /// Gets all tables
        /// </summary>
        /// <returns></returns>
        IQueryable<Table> GetAllTables();

        /// <summary>
        /// Gets table by id
        /// </summary>
        /// <param name="id">Table Id</param>
        /// <returns>Table entity</returns>
        Table GetById(Int64 id);

        /// <summary>
        /// Removes the table
        /// </summary>
        /// <param name="table">Table entity</param>
        void Remove(Table table);
    }
}
