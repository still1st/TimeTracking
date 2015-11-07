using TimeTracking.Domain.DataAccess.Base;
using TimeTracking.Domain.Models;

namespace TimeTracking.Domain.DataAccess.Repositories.Impl
{
    public class HolidayRepositoryImpl : RepositoryBase<Holiday>, IHolidayRepository
    {
        public HolidayRepositoryImpl(IContextFactory contextFactory)
            : base(contextFactory)
        {

        }
    }
}
