using System.Threading.Tasks;
using AutoMapper;
using Witcherius.BL.Services.Common;
using Witcherius.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Witcherius.BL.DataTransferObjects.Item;

namespace Witcherius.BL.Services.Item
{
    public class ItemService : CrudService<Entities.Item, ItemDto, ItemDto, ItemEditDto>, IItemService
    {
        public ItemService(IMapper mapper, IRepository<Entities.Item> repository) : base(mapper, repository)
        {
        }

        protected override async Task<Entities.Item> GetWithIncludesAsync(Guid entityId)
        {
            return await Repository.GetAsync(entityId, nameof(Entities.Item.Attributes));
        }

        public async Task<IEnumerable<ItemDto>> ListAllForRequiredLevel(int level)
        {
            var upperBound = level;
            upperBound += (level % 4 == 0) ? 3 : level % 4;
            Expression<Func<Entities.Item, bool>> expr = a => (a.RequiredLevel>=level && a.RequiredLevel<=upperBound);
            var list = await Repository.GetAllAsync(expr);
            return list.Select(item => Mapper.Map<ItemDto>(item)).ToList();
        }
    }
}
