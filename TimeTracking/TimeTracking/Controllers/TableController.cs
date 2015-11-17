using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using TimeTracking.Domain.DataAccess.Base;
using TimeTracking.Domain.Models;
using TimeTracking.Models;
using TimeTracking.Services;

namespace TimeTracking.Controllers
{
    [RoutePrefix("api/table")]
    public class TableController : ApiController
    {
        public TableController(IEmployeeService employeeService,
            ITableService tableService,
            IUnitOfWork unitOfWork)
        {
            _employeeService = employeeService;
            _tableService = tableService;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IHttpActionResult GetTables()
        {
            var tables = Mapper.Map<IEnumerable<TableInfoModel>>(_tableService.GetAllTables().OrderBy(x => x.Year).ThenBy(x => x.Month));
            return Ok(tables);
        }

        [HttpPost]
        public IHttpActionResult PostTable(TableModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var employees = _employeeService.GetAllEmployees().ToDictionary(k => k.EmployeeId, v => v);

            var table = Mapper.Map<Table>(model);
            table.Records = new List<TableRecord>();

            foreach (var employeeTable in model.Employees)
            {
                if(!employees.ContainsKey(employeeTable.EmployeeId))
                    throw new Exception("Employee with key " + employeeTable.EmployeeId + " wasn't found");

                foreach (var recordModel in employeeTable.Records)
                {
                    var record = Mapper.Map<TableRecord>(recordModel);
                    record.Date = new DateTime(table.Year, table.Month, record.DayNumber);
                    record.Employee = employees[employeeTable.EmployeeId];
                    record.Table = table;

                    table.Records.Add(record);
                }
            }

            _tableService.AddTable(table);
            _unitOfWork.Commit();

            return Ok();
        }

        [HttpGet]
        [Route("calcMonth")]
        public IHttpActionResult CalcMonth(Int32 year, Int32 month, Int64 employeeId)
        {
            var employee = _employeeService.GetEmployeeById(employeeId);
            if (employee == null)
                return NotFound();

            var records = _tableService.CalcMonth(year, month, employee);
            var plan = records.Sum(x => x.Hours);

            var model = new EmployeeTableModel
            {
                EmployeeId = employee.EmployeeId,
                EmployeeName = employee.LastName + " " + employee.FirstName + " " + employee.MiddleName,
                Plan = plan,
                Fact = plan,
                Records = Mapper.Map<IEnumerable<TableRecordModel>>(records)
            };

            return Ok(model);
        }

        #region private fields
        private IEmployeeService _employeeService;
        private ITableService _tableService;
        private IUnitOfWork _unitOfWork; 
        #endregion
    }
}
