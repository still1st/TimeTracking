using System;

namespace TimeTracking.Domain.Models
{
    public class TableRecord
    {
        public virtual Int64 TableRecordId { get; set; }

        /// <summary>
        /// Gets or sets the number of working hours
        /// </summary>
        public virtual Double? Hours { get; set; }

        /// <summary>
        /// Gets or sets number of the day
        /// </summary>
        public virtual Int32 DayNumber { get; set; }

        /// <summary>
        /// Gets or sets the date of the record
        /// </summary>
        public virtual DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the type of the record
        /// </summary>
        public virtual TableRecordType Type { get; set; }

        /// <summary>
        /// Gets or sets the table
        /// </summary>
        public virtual Table Table { get; set; }

        /// <summary>
        /// Gets or sets the employee
        /// </summary>
        public virtual Employee Employee { get; set; }
    }
}
