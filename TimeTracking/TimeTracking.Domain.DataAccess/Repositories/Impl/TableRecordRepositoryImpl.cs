using TimeTracking.Domain.DataAccess.Base;
using TimeTracking.Domain.Models;

namespace TimeTracking.Domain.DataAccess.Repositories.Impl
{
    public class TableRecordRepositoryImpl : RepositoryBase<TableRecord>, ITableRecordRepository
    {
        public TableRecordRepositoryImpl(IContextFactory contextFactory)
            : base(contextFactory)
        {

        }
    }
}
