using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using TimeTracking.Domain.Models;
using TimeTracking.Services.Impl;

namespace TimeTracking.Services.Tests
{
    [TestClass]
    public class ReportServiceTests
    {
        private IReportService _reportService;

        [TestInitialize]
        public void Init()
        {
            _reportService = new ReportServiceImpl();
        }

        [TestMethod]
        public void BuildMothlyTable_ShouldReturnMothlyReport()
        {
            // ARRANGE
            var nurse = new Employee { FirstName = "Emma", LastName = "Smith", Post = EmployeePost.Nurse };

            var table = new Table { Year = 2015, Month = 8 };
            table.Records = GetTableRecords(table, nurse);

            // ACT
            var report = _reportService.BuildReport(table);

            // ASSERT
            Assert.AreEqual(1, report.EmployeeLines.Count);

            var employeeLine = report.EmployeeLines.First();
            Assert.AreEqual(26, employeeLine.TotalAppeareance);
            Assert.AreEqual(161,7, employeeLine.TotalHours);

            var firstWeek = employeeLine.FirstWeek;
            Assert.AreEqual(13, firstWeek.TotalAppereance);
            Assert.AreEqual(78.4, firstWeek.TotalHours, 0.01);

            var secondWeek = employeeLine.SecondWeek;
            Assert.AreEqual(13, secondWeek.TotalAppereance);
            Assert.AreEqual(83.3, secondWeek.TotalHours, 0.01);
        }

        [TestMethod]
        public void BuildExcelReport()
        {
            // ARRANGE
            var nurse = new Employee { FirstName = "Emma", LastName = "Smith", Post = EmployeePost.Nurse };

            var table = new Table { Year = 2015, Month = 8 };
            table.Records = GetTableRecords(table, nurse);

            // ACT
            var report = _reportService.BuildReport(table);
            var excel = _reportService.GetExcel(report);
            excel.SaveAs(@"D:\_delete\report.xlsx");


            // ASSERT
            Assert.IsNotNull(excel);
        }

        [Ignore]
        public ICollection<TableRecord> GetTableRecords(Table table, Employee employee)
        {
            var records = new List<TableRecord>();

            records.Add(GetTableRecord(table, employee, 1, 3.8, TableRecordType.Appearance));
            records.Add(GetTableRecord(table, employee, 2, 0, TableRecordType.DayOff));
            records.Add(GetTableRecord(table, employee, 3, 6.7, TableRecordType.Appearance));
            records.Add(GetTableRecord(table, employee, 4, 6.7, TableRecordType.Appearance));
            records.Add(GetTableRecord(table, employee, 5, 6.7, TableRecordType.Appearance));
            records.Add(GetTableRecord(table, employee, 6, 6.7, TableRecordType.Appearance));
            records.Add(GetTableRecord(table, employee, 7, 6.7, TableRecordType.Appearance));
            records.Add(GetTableRecord(table, employee, 8, 3.8, TableRecordType.Appearance));
            records.Add(GetTableRecord(table, employee, 9, 0, TableRecordType.DayOff));
            records.Add(GetTableRecord(table, employee, 10, 6.7, TableRecordType.Appearance));
            records.Add(GetTableRecord(table, employee, 11, 6.7, TableRecordType.Appearance));
            records.Add(GetTableRecord(table, employee, 12, 6.7, TableRecordType.Appearance));
            records.Add(GetTableRecord(table, employee, 13, 6.7, TableRecordType.Appearance));
            records.Add(GetTableRecord(table, employee, 14, 6.7, TableRecordType.Appearance));
            records.Add(GetTableRecord(table, employee, 15, 3.8, TableRecordType.Appearance));
            records.Add(GetTableRecord(table, employee, 16, 0, TableRecordType.DayOff));
            records.Add(GetTableRecord(table, employee, 17, 6.7, TableRecordType.Appearance));
            records.Add(GetTableRecord(table, employee, 18, 6.7, TableRecordType.Appearance));
            records.Add(GetTableRecord(table, employee, 19, 6.7, TableRecordType.Appearance));
            records.Add(GetTableRecord(table, employee, 20, 6.7, TableRecordType.Appearance));
            records.Add(GetTableRecord(table, employee, 21, 6.7, TableRecordType.Appearance));
            records.Add(GetTableRecord(table, employee, 22, 4.8, TableRecordType.Appearance));
            records.Add(GetTableRecord(table, employee, 23, 0, TableRecordType.DayOff));
            records.Add(GetTableRecord(table, employee, 24, 6.7, TableRecordType.Appearance));
            records.Add(GetTableRecord(table, employee, 25, 6.7, TableRecordType.Appearance));
            records.Add(GetTableRecord(table, employee, 26, 6.7, TableRecordType.Appearance));
            records.Add(GetTableRecord(table, employee, 27, 6.7, TableRecordType.Appearance));
            records.Add(GetTableRecord(table, employee, 28, 6.7, TableRecordType.Appearance));
            records.Add(GetTableRecord(table, employee, 29, 4.8, TableRecordType.Appearance));
            records.Add(GetTableRecord(table, employee, 30, 0, TableRecordType.DayOff));
            records.Add(GetTableRecord(table, employee, 31, 6.7, TableRecordType.Appearance));

            return records;
        }

        [Ignore]
        private TableRecord GetTableRecord(Table table, Employee employee, Int32 day, Double hours, TableRecordType type)
        {
            return new TableRecord
            {
                Table = table,
                Employee = employee,
                DayNumber = day,
                Date = new DateTime(table.Year, table.Month, day),
                Hours = hours,
                Type = type
            };
        }
    }
}
