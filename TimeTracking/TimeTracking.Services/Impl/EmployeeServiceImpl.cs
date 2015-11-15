using System;
using System.Linq;
using TimeTracking.Domain.DataAccess.Repositories;
using TimeTracking.Domain.Enums;
using TimeTracking.Domain.Models;

namespace TimeTracking.Services.Impl
{
    public class EmployeeServiceImpl : IEmployeeService
    {
        /// <summary>
        /// Initializes a new instance of <see cref="EmployeeServiceImpl"/>
        /// </summary>
        /// <param name="employeeRepository"></param>
        public EmployeeServiceImpl(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        /// <summary>
        /// Gets a employee by ID
        /// </summary>
        /// <param name="employeeId">Employee ID</param>
        /// <returns>Found employee entity</returns>
        public Employee GetEmployeeById(Int64 employeeId)
        {
            return _employeeRepository.GetById(employeeId);
        }

        /// <summary>
        /// Gets all employees
        /// </summary>
        /// <returns></returns>
        public IQueryable<Employee> GetAllEmployees()
        {
            return _employeeRepository.Query();
        }

        /// <summary>
        /// Resolves the employee group by the employee post
        /// </summary>
        /// <param name="post">Employee post</param>
        /// <returns>Resolved employee group</returns>
        public EmployeeGroup ResolveGroupByPost(EmployeePost post)
        {
            if (post == EmployeePost.JuniorNurse)
                return EmployeeGroup.JuniorStaff;

            return EmployeeGroup.MiddleStaffAndDoctors;
        }

        #region private fields
        private IEmployeeRepository _employeeRepository; 
        #endregion
    }
}
