using AutoMapper;
using System;
using System.Collections.Generic;
using System.Web.Http;
using TimeTracking.Models;
using TimeTracking.Services;

namespace TimeTracking.Controllers
{
    [RoutePrefix("api/table")]
    public class TableController : ApiController
    {
        public TableController(IEmployeeService employeeService,
            ITableService tableService)
        {
            _employeeService = employeeService;
            _tableService = tableService;
        }

        [HttpGet]
        [Route("calcMonth")]
        public IHttpActionResult CalcMonth(Int32 year, Int32 month, Int64 employeeId)
        {
            var employee = _employeeService.GetEmployeeById(employeeId);
            if (employee == null)
                return NotFound();

            var records = _tableService.CalcMonth(year, month, employee);

            return Ok(Mapper.Map<IEnumerable<TableRecordModel>>(records));
        }

        #region private fields
        private IEmployeeService _employeeService;
        private ITableService _tableService; 
        #endregion
    }
}
