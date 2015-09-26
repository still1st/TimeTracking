using System;

namespace TimeTracking.Domain.Models
{
    public class Employee
    {
        public virtual Int64 EmployeeId { get; set; }

        public virtual String FirstName { get; set; }
        
        public virtual String LastName { get; set; }

        public virtual EmployeePost Post { get; set; }
    }
}
