using TimeTracking.Domain.DataAccess.Base;
using TimeTracking.Domain.Models;

namespace TimeTracking.Domain.DataAccess.Repositories.Impl
{
    public class EmployeeRepositoryImpl : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepositoryImpl(IContextFactory contextFactory)
            : base(contextFactory)
        {

        }
    }
}
