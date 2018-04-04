using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Witcherius.Infrastructure.UnitOfWork
{
    public class EntityFrameworkUnitOfWork : UnitOfWorkBase
    {
        public DbContext Context { get; }

        public EntityFrameworkUnitOfWork(Func<DbContext> dbContextFactory)
        {
            try
            {
                Context = dbContextFactory?.Invoke();
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Db context factory cant be null! {ex.Message}");
            }
        }

        protected override async Task CommitCore()
        {
            await Context.SaveChangesAsync();
        }

        public override void Dispose()
        {
            Context.Dispose();
        }

    }
}
