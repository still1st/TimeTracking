using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracking.Domain.DataAccess.Base;
using TimeTracking.Domain.Models;

namespace TimeTracking.Domain.DataAccess.Repositories.Impl
{
    public class PlanWorkDayRepositoryImpl : RepositoryBase<PlanWorkDay>, IPlanWorkDayRepository
    {
        public PlanWorkDayRepositoryImpl(IContextFactory contextFactory)
            : base(contextFactory)
        {

        }
    }
}
