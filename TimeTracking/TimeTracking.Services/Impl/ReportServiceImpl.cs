using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using TimeTracking.Common.Extensions;
using TimeTracking.Domain.Models;
using TimeTracking.Services.Reports;

namespace TimeTracking.Services.Impl
{
    public class ReportServiceImpl : IReportService
    {
        #region constants
        private const Int32 RECORDS_COUNT_OF_WEEK = 15;
        #endregion

        /// <summary>
        /// Builds report
        /// </summary>
        /// <param name="table"><see cref="Table"/> entity</param>
        /// <returns><see cref="Report"/> instance</returns>
        public Report BuildReport(Table table)
        {
            if (table == null)
                throw new ArgumentNullException("table");

            if (table.Records.IsEmpty())
                throw new ArgumentException("table.records");

            var report = new Report();
            report.Year = table.Year;
            report.Month = table.Month;

            foreach (var groupedRecords in table.Records.GroupBy(x => x.Employee))
            {
                var employeeLine = new Report.EmployeeLine();
                employeeLine.FullName = groupedRecords.Key.LastName + " " + groupedRecords.Key.FirstName;
                employeeLine.Post = groupedRecords.Key.Post.ToString();

                employeeLine.FirstWeek.Records = groupedRecords.Take(RECORDS_COUNT_OF_WEEK);
                employeeLine.SecondWeek.Records = groupedRecords.Skip(RECORDS_COUNT_OF_WEEK).Take(groupedRecords.Count() - RECORDS_COUNT_OF_WEEK);

                CalcWeek(employeeLine.FirstWeek);
                CalcWeek(employeeLine.SecondWeek);
                CalcTotal(employeeLine);

                report.EmployeeLines.Add(employeeLine);
            }

            return report;
        }

        /// <summary>
        /// Gets excel document
        /// </summary>
        /// <param name="report"><see cref="Report"/> instance</param>
        /// <returns></returns>
        public XLWorkbook GetExcel(Report report)
        {
            if (report == null)
                throw new ArgumentNullException("report");

            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add(report.Year + "." + report.Month);
            
            var employeeLines = report.EmployeeLines;
            var count = employeeLines.Count;
            for(var i = 0; i < count; i++)
            {
                var row = worksheet.Row(i + 1);
                row.Cell("A").SetValue(employeeLines[i].FullName);

                worksheet.Range(String.Format("A{0}:A{0}", 1, 4)).Merge();
            }

            return workbook;
        }

        #region private methods
        private void CalcWeek(Report.WeekLine week)
        {
            week.TotalAppereance = week.Records.Count(x => x.Type == TableRecordType.Appearance);
            week.TotalHours = week.Records.Where(x => x.Hours.HasValue).Sum(x => x.Hours.Value);
        }

        private void CalcTotal(Report.EmployeeLine employee)
        {
            var records = new List<TableRecord>();
            records.AddRange(employee.FirstWeek.Records);
            records.AddRange(employee.SecondWeek.Records);

            employee.TotalAppeareance = records.Count(x => x.Type == TableRecordType.Appearance);
            employee.TotalHours = records.Where(x => x.Hours.HasValue).Sum(x => x.Hours.Value);
        }
        #endregion
    }
}
