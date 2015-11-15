using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TimeTracking.Models
{
    public class EmployeeModel
    {
        public Int64 EmployeeId { get; set; }
        [Required]
        public String FirstName { get; set; }
        [Required]
        public String MiddleName { get; set; }
        [Required]
        public String LastName { get; set; }

        public String FullName { get; set; }

        [Required]
        public Int32 PostId { get; set; }
        public String Post { get; set; }

        public Int32 GroupId { get; set; }
        public String Group { get; set; }
    }
}