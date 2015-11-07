using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TimeTracking.Models
{
    public class HolidayModel
    {
        public Int64 HolidayId { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public Int32 TypeId { get; set; }
        public String Type { get; set; }
    }
}