using System.Data.Entity;
using TimeTracking.Domain.Models;

namespace TimeTracking.Domain.DataAccess
{
    public class TimeTrackingContext : DbContext
    {
        public TimeTrackingContext()
            : base("TimeTrackingConnection")
        {
        }

        /// <summary>
        /// Tables set
        /// </summary>
        public DbSet<Table> Tables { get; set; }

        /// <summary>
        /// Employees set
        /// </summary>
        public DbSet<Employee> Employees { get; set; }

        /// <summary>
        /// Holidays and preholidays set
        /// </summary>
        public DbSet<Holiday> Holidays { get; set; }

        /// <summary>
        /// Plan workdays set for medical stuff
        /// </summary>
        public DbSet<PlanWorkDay> PlanWorkDays { get; set; }
    }
}
