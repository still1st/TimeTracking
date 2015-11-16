using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracking.Domain.DataAccess.Repositories;
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
            IPlanWorkDayRepository planWorkDayrepository)
        {
            _holidayRepository = holidayRepository;
            _planWorkDayrepository = planWorkDayrepository;
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

            var planWorktime = _planWorkDayrepository.Query().FirstOrDefault(x => x.EmployeeGroup == employee.Group);
            if(planWorktime == null)
                throw new InvalidOperationException("Planworktime wasn't settled");

            var allDays = _holidayRepository.Query().Where(x => x.Date.Year == year && x.Date.Month == month).ToList();
            var holidays = allDays.Where(x => x.Type == Domain.Enums.DayType.Holiday);
            var preholidays = allDays.Where(x => x.Type == Domain.Enums.DayType.Preholiday);

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
        private IPlanWorkDayRepository _planWorkDayrepository; 
        #endregion
    }
}
