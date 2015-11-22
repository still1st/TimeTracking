using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracking.Domain.DataAccess.Repositories;
using TimeTracking.Domain.Enums;
using TimeTracking.Domain.Models;

namespace TimeTracking.Services.Impl
{
    public class TableServiceImpl : ITableService
    {
        /// <summary>
        /// Initializes a new instance of <see cref="TableServiceImpl"/>
        /// </summary>
        /// <param name="holidayRepository"></param>
        /// <param name="planWorkDayrepository"></param>
        public TableServiceImpl(IHolidayRepository holidayRepository,
            IPlanWorkDayRepository planWorkDayrepository,
            ITableRepository tableRepository,
            ITableRecordRepository tableRecordRepository)
        {
            _holidayRepository = holidayRepository;
            _planWorkDayRepository = planWorkDayrepository;
            _tableRepository = tableRepository;
            _tableRecordRepository = tableRecordRepository;
        }

        /// <summary>
        /// Adds a new table
        /// </summary>
        /// <param name="table">Table entity</param>
        public void AddTable(Table table)
        {
            if (table == null)
                throw new ArgumentNullException("table");

            _tableRepository.Add(table);
            _tableRecordRepository.AddRange(table.Records);
        }

        /// <summary>
        /// Gets all tables
        /// </summary>
        /// <returns></returns>
        public IQueryable<Table> GetAllTables()
        {
            return _tableRepository.Query();
        }

        /// <summary>
        /// Gets table by id
        /// </summary>
        /// <param name="id">Table Id</param>
        /// <returns>Table entity</returns>
        public Table GetById(Int64 id)
        {
            return _tableRepository.GetById(id);
        }

        /// <summary>
        /// Removes the table
        /// </summary>
        /// <param name="table">Table entity</param>
        public void Remove(Table table)
        {
            if (table == null)
                throw new ArgumentNullException("table");

            _tableRecordRepository.DeleteRange(table.Records);
            _tableRepository.Delete(table);
        }

        /// <summary>
        /// Calcs work time for the employee for the month and year
        /// </summary>
        /// <param name="year">Year</param>
        /// <param name="month">Month</param>
        /// <param name="employee">Employee</param>
        /// <returns>Collection of the table records</returns>
        public IEnumerable<TableRecord> CalcMonth(Int32 year, Int32 month, Employee employee)
        {
            if (employee == null)
                throw new ArgumentNullException("employee");

            if (month <= 0 || month > 12)
                throw new ArgumentException("month");

            var planWorktime = _planWorkDayRepository.Query().FirstOrDefault(x => x.EmployeeGroup == employee.Group);
            if(planWorktime == null)
                throw new InvalidOperationException("Planworktime wasn't settled");

            var holidays = GetHolidays(year, month);
            var preholidays = GetPreholidays(year, month);

            var tableRecords = new List<TableRecord>();

            var days = DateTime.DaysInMonth(year, month);
            var startDay = new DateTime(year, month, 1);
            var endDay = startDay.AddDays(days - 1);

            for (; startDay <= endDay; startDay = startDay.AddDays(1.0))
            {
                if (IsWeekend(startDay) || IsHoliday(startDay, holidays))
                {
                    tableRecords.Add(CreateTableRecord(startDay, employee));
                    continue;
                }

                var hours = planWorktime.Hours;
                if (IsPreHoliday(startDay, preholidays))
                    hours -= 1.0;

                tableRecords.Add(CreateTableRecord(startDay, employee, hours, TableRecordType.Appearance));
            }

            return tableRecords;
        }

        /// <summary>
        /// Calc work time for the month and for the employee group
        /// </summary>
        /// <param name="year">Year</param>
        /// <param name="month">Month</param>
        /// <param name="planWorkday">Employee group</param>
        /// <returns>PlanWorkmonth</returns>
        public Double CalcMonth(Int32 year, Int32 month, EmployeeGroup group)
        {
            var planWorkday = _planWorkDayRepository.Query().FirstOrDefault(x => x.EmployeeGroup == group);
            return CalcMonth(year, month, planWorkday.Hours);
        }

        /// <summary>
        /// Calc work time for the month and for the planWorkday
        /// </summary>
        /// <param name="year">Year</param>
        /// <param name="month">Month</param>
        /// <param name="planWorkday">Plan workday</param>
        /// <returns>PlanWorkmonth</returns>
        public Double CalcMonth(Int32 year, Int32 month, Double planWorkday)
        {
            var days = DateTime.DaysInMonth(year, month);
            var startDay = new DateTime(year, month, 1);
            var endDay = startDay.AddDays(days - 1);

            Double plan = 0.0;

            var holidays = GetHolidays(year, month);
            var preholidays = GetPreholidays(year, month);

            for (; startDay <= endDay; startDay = startDay.AddDays(1.0))
            {
                if (IsWeekend(startDay) || IsHoliday(startDay, holidays))
                    continue;

                plan += IsPreHoliday(startDay, preholidays) ? planWorkday - 1.0 : planWorkday;
            }

            return plan;
        }

        private TableRecord CreateTableRecord(DateTime date, Employee employee, Double hours = 0.0, TableRecordType type = TableRecordType.DayOff)
        {
            return new TableRecord
            {
                Date = date,
                DayNumber = date.Day,
                Employee = employee,
                Hours = hours,
                Type = type
            };
        }

        private List<Holiday> GetHolidays(Int32 year, Int32 month)
        {
            return _holidayRepository.Query().Where(x => x.Date.Year == year && x.Date.Month == month && x.Type == Domain.Enums.DayType.Holiday).ToList();
        }

        private List<Holiday> GetPreholidays(Int32 year, Int32 month)
        {
            return _holidayRepository.Query().Where(x => x.Date.Year == year && x.Date.Month == month && x.Type == Domain.Enums.DayType.Preholiday).ToList();
        }

        private Boolean IsPreHoliday(DateTime date, IEnumerable<Holiday> preholidays)
        {
            return preholidays.Any(x => x.Date == date);
        }

        private Boolean IsHoliday(DateTime date, IEnumerable<Holiday> holidays)
        {
            return holidays.Any(x => x.Date == date);
        }

        private Boolean IsWeekend(DateTime date)
        {
            return date.DayOfWeek == DayOfWeek.Sunday || date.DayOfWeek == DayOfWeek.Saturday;
        }

        #region private fields
        private IHolidayRepository _holidayRepository;
        private IPlanWorkDayRepository _planWorkDayRepository;
        private ITableRepository _tableRepository;
        private ITableRecordRepository _tableRecordRepository; 
        #endregion
    }
}
