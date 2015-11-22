using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using TimeTracking.Common.Extensions;
using TimeTracking.Domain.DataAccess.Base;
using TimeTracking.Domain.DataAccess.Repositories;
using TimeTracking.Domain.Enums;
using TimeTracking.Domain.Models;
using TimeTracking.Models;
using TimeTracking.Services;

namespace TimeTracking.Controllers
{
    [RoutePrefix("api/employee")]
    public class EmployeeController : ApiController
    {
        public EmployeeController(IEmployeeService employeeService,
            IEmployeeRepository employeeRepository,
            ITableRecordRepository tableRecordRepository,
            IUnitOfWork unitOfWork)
        {
            _employeeService = employeeService;
            _employeeRepository = employeeRepository;
            _tableRecordRepository = tableRecordRepository;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok(Mapper.Map<IEnumerable<EmployeeModel>>(_employeeRepository.Query().ToList()));
        }

        [HttpPost]
        public IHttpActionResult Post(EmployeeModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var employee = Mapper.Map<Employee>(model);
            employee.Group = _employeeService.ResolveGroupByPost(employee.Post);
            _employeeRepository.Add(employee);
            _unitOfWork.Commit();

            return Ok(Mapper.Map<EmployeeModel>(employee));
        }

        [HttpDelete]
        public IHttpActionResult Delete(Int64 id)
        {
            var employee = _employeeRepository.GetById(id);
            if (employee == null)
                return NotFound();

            if (_tableRecordRepository.Query().Any(x => x.Employee.EmployeeId == id))
                return BadRequest("There are tables for the employee " + id);

            _employeeRepository.Delete(employee);
            _unitOfWork.Commit();

            return Ok();
        }

        [HttpGet]
        [Route("Posts")]
        public IHttpActionResult GetPosts()
        {
            return Ok(EnumExtensions.ToKeyValuePairs<EmployeePost>());
        }

        [HttpGet]
        [Route("Groups")]
        public IHttpActionResult GetGroups()
        {
            return Ok(EnumExtensions.ToKeyValuePairs<EmployeeGroup>());
        }

        #region private fields
        private IEmployeeRepository _employeeRepository;
        private IUnitOfWork _unitOfWork;
        private IEmployeeService _employeeService;
        private ITableRecordRepository _tableRecordRepository;
        #endregion
    }
}
