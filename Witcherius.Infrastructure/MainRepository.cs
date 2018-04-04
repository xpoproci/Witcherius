using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Witcherius.Infrastructure.UnitOfWork;

namespace Witcherius.Infrastructure
{
    public class MainRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity, new()
    {
        private readonly IUnitOfWorkProvider _provider;
        protected DbContext Context => ((EntityFrameworkUnitOfWork)_provider.GetUnitOfWorkInstance()).Context;

        public MainRepository(IUnitOfWorkProvider provider)
        {
            _provider = provider;
        }

        public async Task<TEntity> GetAsync(Guid id)
        {
            return await Context.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity> GetAsync(Guid id, params string[] includes)
        {
            var ctx = Context.Set<TEntity>();
            foreach (var include in includes)
            {
                ctx.Include(include);
            }
            return await ctx.FirstOrDefaultAsync(e => e.Id == id);
        }

        public Guid Create(TEntity entity)
        {
            entity.Id = Guid.NewGuid();
            Context.Set<TEntity>().Add(entity);
            return entity.Id;
        }

        public void Update(TEntity entity)
        {
            var e = Context.Set<TEntity>().Attach(entity);
            Context.Entry(e).State = EntityState.Modified;
        }

        public void Delete(Guid id)
        {
            var entity = Context.Set<TEntity>().Find(id);
            if (entity != null)
            {
                Context.Set<TEntity>().Remove(entity);
            }
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return new List<TEntity>(await Context.Set<TEntity>().ToListAsync());
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await Context.Set<TEntity>().Where(expression).ToListAsync();
        }

    }
}
