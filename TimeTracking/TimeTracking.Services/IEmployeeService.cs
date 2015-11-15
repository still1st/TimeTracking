using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracking.Domain.Enums;
using TimeTracking.Domain.Models;

namespace TimeTracking.Services
{
    public interface IEmployeeService
    {
        /// <summary>
        /// Gets a employee by ID
        /// </summary>
        /// <param name="employeeId">Employee ID</param>
        /// <returns>Found employee entity</returns>
        Employee GetEmployeeById(Int64 employeeId);

        /// <summary>
        /// Gets all employees
        /// </summary>
        /// <returns></returns>
        IQueryable<Employee> GetAllEmployees();

        /// <summary>
        /// Resolves the employee group by the employee post
        /// </summary>
        /// <param name="post">Employee post</param>
        /// <returns>Resolved employee group</returns>
        EmployeeGroup ResolveGroupByPost(EmployeePost post);
    }
}
