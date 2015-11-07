using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracking.Domain.DataAccess.Base;
using TimeTracking.Domain.Models;

namespace TimeTracking.Domain.DataAccess.Repositories
{
    public interface IPlanWorkDayRepository : IRepository<PlanWorkDay>
    {
    }
}
