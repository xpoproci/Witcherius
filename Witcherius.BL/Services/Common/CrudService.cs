using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Witcherius.BL.DataTransferObjects.Common;
using Witcherius.Infrastructure;

namespace Witcherius.BL.Services.Common
{
    public abstract class CrudService<TEntity, TDto, TCreateDto, TEditDto> : ServicesBase 
        where TEntity : class, IEntity, new ()
        where TDto : BaseDto
        where TCreateDto : BaseDto
        where TEditDto : BaseDto
    {
        protected readonly IRepository<TEntity> Repository;

        protected CrudService(IMapper mapper, IRepository<TEntity> repository) : base(mapper)
        {
            Repository = repository;
        }

        public virtual async Task<TDto> GetAsync(Guid entityId, bool withIncludes = true)
        {
            TEntity entity;
            if (withIncludes)
            {
                entity = await GetWithIncludesAsync(entityId);
            }
            else
            {
                entity = await Repository.GetAsync(entityId);
            }
            return entity != null ? Mapper.Map<TDto>(entity) : null;
        }

        protected abstract Task<TEntity> GetWithIncludesAsync(Guid entityId);

        /// <summary>
        /// Creates new entity
        /// </summary>
        /// <param name="entityDto">entity details</param>
        public virtual Guid Create(TCreateDto entityDto)
        {
            var entity = Mapper.Map<TEntity>(entityDto);            
            return Repository.Create(entity);
        }

        /// <summary>
        /// Updates entity
        /// </summary>
        /// <param name="entityDto">entity details</param>
        public virtual async Task Update(TEditDto entityDto)
        {
            var entity = await GetWithIncludesAsync(entityDto.Id);
            Mapper.Map(entityDto, entity);
            Repository.Update(entity);
        }

        /// <summary>
        /// Deletes entity with given Id
        /// </summary>
        /// <param name="entityId">Id of the entity to delete</param>
        public virtual void Delete(Guid entityId)
        {
            Repository.Delete(entityId);
        }

        /// <summary>
        /// Gets all DTOs (for given expression)
        /// </summary>
        /// <param name="expression"></param>
        public virtual async Task<IEnumerable<TDto>> ListAllAsync(Expression<Func<TEntity, bool>> expression)
        {
            var l = await Repository.GetAllAsync(expression);
            return l.Select(entity => Mapper.Map<TDto>(entity)).ToList();
        }

        /// <summary>
        /// Gets all DTOs (for given type)
        /// </summary>
        /// <returns>all available dtos (for given type)</returns>
        public virtual async Task<IEnumerable<TDto>> ListAllAsync()
        {
            var l = await Repository.GetAllAsync();
            return l.Select(entity => Mapper.Map<TDto>(entity)).ToList();
        }
    }
}
