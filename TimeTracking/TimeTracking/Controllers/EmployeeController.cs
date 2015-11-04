using AutoMapper;
using AutoMapper.QueryableExtensions;
using System.Linq;
using System;
using System.Web.Http;
using TimeTracking.Common.Extensions;
using TimeTracking.Domain.DataAccess.Base;
using TimeTracking.Domain.DataAccess.Repositories;
using TimeTracking.Domain.Models;
using TimeTracking.Models;
using System.Collections.Generic;

namespace TimeTracking.Controllers
{
    [RoutePrefix("api/employee")]
    public class EmployeeController : ApiController
    {
        public EmployeeController(IEmployeeRepository employeeRepository,
            IUnitOfWork unitOfWork)
        {
            _employeeRepository = employeeRepository;
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
            _employeeRepository.Add(employee);
            _unitOfWork.Commit();

            return Ok(Mapper.Map<EmployeeModel>(employee));
        }

        [HttpPut]
        public IHttpActionResult Put(EmployeeModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var employee = _employeeRepository.GetById(model.EmployeeId);
            if (employee == null)
                return NotFound();

            employee.FirstName = model.FirstName;
            employee.LastName = model.LastName;
            employee.Post = (EmployeePost)model.PostId;

            _employeeRepository.Update(employee);
            _unitOfWork.Commit();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Delete(Int64 employeeId)
        {
            var employee = _employeeRepository.GetById(employeeId);
            if (employee == null)
                return NotFound();

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

        #region private fields
        private IEmployeeRepository _employeeRepository;
        private IUnitOfWork _unitOfWork; 
        #endregion
    }
}
