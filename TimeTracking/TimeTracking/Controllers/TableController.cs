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
        public IHttpActionResult GetTable(Int64 id)
        {
            var table = _tableService.GetById(id);
            if (table == null)
                return NotFound();

            var model = Mapper.Map<TableModel>(table);
            var employeeTableModels = new List<EmployeeTableModel>();

            var employees = table.Records.GroupBy(x => x.Employee);
            foreach (var employee in employees)
            {
                var employeeTableModel = Mapper.Map<EmployeeTableModel>(employee.Key);
                employeeTableModel.Records = Mapper.Map<IEnumerable<TableRecordModel>>(employee);
                employeeTableModel.Plan = _tableService.CalcMonth(table.Year, table.Month, employee.Key.Group);
                employeeTableModel.Fact = employeeTableModel.Records.Sum(x => x.Hours);

                employeeTableModels.Add(employeeTableModel);
            }

            model.Employees = employeeTableModels;

            return Ok(model);
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
                if (!employees.ContainsKey(employeeTable.EmployeeId))
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

        [HttpPut]
        public IHttpActionResult PutTable(Int64 id, [FromBody]TableModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (id != model.TableId)
                return BadRequest();

            var table = _tableService.GetById(id);
            if (table == null)
                return NotFound();

            var employees = _employeeService.GetAllEmployees().ToDictionary(k => k.EmployeeId, v => v);

            Mapper.Map<TableModel, Table>(model, table);

            // Update/add records
            var groupedEmployees = table.Records.GroupBy(x => x.Employee);
            foreach (var employeeModel in model.Employees)
            {
                if (!employees.ContainsKey(employeeModel.EmployeeId))
                    throw new Exception("Employee with id " + employeeModel.EmployeeId + " wasn't found");

                var employee = groupedEmployees.FirstOrDefault(x => x.Key.EmployeeId == employeeModel.EmployeeId);
                foreach (var recordModel in employeeModel.Records)
                {
                    TableRecord record;
                    // add new employee in table
                    if (employee == null)
                    {
                        record = Mapper.Map<TableRecord>(recordModel);
                        record.Date = new DateTime(table.Year, table.Month, recordModel.DayNumber);
                        record.Table = table;
                        record.Employee = employees[employeeModel.EmployeeId];

                        table.Records.Add(record);
                    }
                    // update existing records
                    else
                    {
                        record = employee.FirstOrDefault(x => x.TableRecordId == recordModel.TableRecordId);
                        if (record == null)
                            throw new Exception("Record with id " + recordModel.TableRecordId + "wasn't found");

                        Mapper.Map<TableRecordModel, TableRecord>(recordModel, record);
                    }
                }
            }

            // delete records if it needs
            table.Records.Where(x => !model.Employees.Any(y => y.EmployeeId == x.Employee.EmployeeId))
                .ToList()
                .ForEach(x => table.Records.Remove(x));

            _unitOfWork.Commit();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult DeleteTable(Int64 id)
        {
            var table = _tableService.GetById(id);
            if (table == null)
                return NotFound();

            _tableService.Remove(table);
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
