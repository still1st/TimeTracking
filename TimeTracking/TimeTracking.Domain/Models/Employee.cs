using System;
using TimeTracking.Domain.Enums;

namespace TimeTracking.Domain.Models
{
    public class Employee
    {
        /// <summary>
        /// Gets or sets the employee id
        /// </summary>
        public virtual Int64 EmployeeId { get; set; }

        /// <summary>
        /// Gets or sets the first name
        /// </summary>
        public virtual String FirstName { get; set; }
        
        /// <summary>
        /// Gets or sets the last name
        /// </summary>
        public virtual String LastName { get; set; }

        /// <summary>
        /// Gets or sets the middle name
        /// </summary>
        public virtual String MiddleName { get; set; }

        /// <summary>
        /// Gets or sets the medical post
        /// </summary>
        public virtual EmployeePost Post { get; set; }

        /// <summary>
        /// Gets or sets the medical staff group
        /// </summary>
        public virtual EmployeeGroup Group { get; set; }
    }
}
