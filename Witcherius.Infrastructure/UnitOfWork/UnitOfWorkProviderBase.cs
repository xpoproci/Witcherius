using System;
using System.Threading;

namespace Witcherius.Infrastructure.UnitOfWork
{
    public abstract class UnitOfWorkProviderBase : IUnitOfWorkProvider
    {
        protected readonly AsyncLocal<IUnitOfWork> UowLocalInstance
            = new AsyncLocal<IUnitOfWork>();

        /// <summary>
        /// Creates a new unit of work.
        /// </summary>
        public abstract IUnitOfWork Create();

        /// <summary>
        /// Gets the unit of work in the current scope.
        /// </summary>
        public IUnitOfWork GetUnitOfWorkInstance()
        {
            if (UowLocalInstance != null)
            {
                return UowLocalInstance.Value;
            }
            throw new InvalidOperationException("UoW not created");
        }

        public void Dispose()
        {
            UowLocalInstance.Value?.Dispose();
            UowLocalInstance.Value = null;
        }
    }
}
