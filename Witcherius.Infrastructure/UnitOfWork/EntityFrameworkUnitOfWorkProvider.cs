using System;
using System.Data.Entity;

namespace Witcherius.Infrastructure.UnitOfWork
{
    public class EntityFrameworkUnitOfWorkProvider : UnitOfWorkProviderBase
    {
        private readonly Func<DbContext> _dbContextFactory;

        public EntityFrameworkUnitOfWorkProvider(Func<DbContext> dbContextFactory)
        {
            this._dbContextFactory = dbContextFactory;
        }

        public override IUnitOfWork Create()
        {
            UowLocalInstance.Value = new EntityFrameworkUnitOfWork(_dbContextFactory);
            return UowLocalInstance.Value;
        }
    }
}
