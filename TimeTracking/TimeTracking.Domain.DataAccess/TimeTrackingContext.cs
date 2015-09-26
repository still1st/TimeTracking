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

        public DbSet<Employee> Employees { get; set; }
    }
}
