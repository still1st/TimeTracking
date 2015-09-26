using System;
using System.Data.Entity;
using TimeTracking.Domain.DataAccess;

namespace TimeTracking.Domain.DataAccess.Base
{
    class DefaultContextFactoryImpl : Disposable, IContextFactory
    {
        /// <summary>
        /// Performs actual disposing
        /// </summary>
        protected override void DisposeCore()
        {
            if (_dataContext != null)
                _dataContext.Dispose();
        }

        /// <summary>
        /// Gets the current context from the factory.
        /// </summary>
        /// <returns></returns>
        public DbContext GetContext()
        {
            if (IsDisposed)
                throw new ObjectDisposedException("ContextFactory");

            return _dataContext ?? (_dataContext = new TimeTrackingContext());
        }

        /// <summary>
        /// The cached instacne of the data context
        /// </summary>
        private TimeTrackingContext _dataContext;
    }
}
