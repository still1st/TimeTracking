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
        /// Resolves the employee group by the employee post
        /// </summary>
        /// <param name="post">Employee post</param>
        /// <returns>Resolved employee group</returns>
        EmployeeGroup ResolveGroupByPost(EmployeePost post);
    }
}
