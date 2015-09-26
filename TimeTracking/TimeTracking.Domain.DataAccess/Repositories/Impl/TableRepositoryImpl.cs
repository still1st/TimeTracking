using TimeTracking.Domain.DataAccess.Base;
using TimeTracking.Domain.Models;

namespace TimeTracking.Domain.DataAccess.Repositories.Impl
{
    public class TableRepositoryImpl : RepositoryBase<Table>, ITableRepository
    {
        public TableRepositoryImpl(IContextFactory contextFactory)
            : base(contextFactory)
        {

        }
    }
}
