using System;
using TimeTracking.Domain.Enums;

namespace TimeTracking.Domain.Models
{
    /// <summary>
    /// Holiday or preholiday
    /// </summary>
    public class Holiday
    {
        /// <summary>
        /// Gets or sets the day ID
        /// </summary>
        public virtual Int64 HolidayId { get; set; }

        /// <summary>
        /// Gets or sets the full date
        /// </summary>
        public virtual DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the day type
        /// </summary>
        public virtual DayType Type { get; set; }
    }
}
