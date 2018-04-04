using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Witcherius.BL.DataTransferObjects.InventoryItems;
using Witcherius.BL.Services.Common;
using Witcherius.Infrastructure;

namespace Witcherius.BL.Services.InventoryItems
{
    public class InventoryItemsService : CrudService<Entities.InventoryItems, InventoryItemsDto, InventoryItemsDto, InventoryItemsDto>, IInventoryItemsService
    {
        public InventoryItemsService(IMapper mapper, IRepository<Entities.InventoryItems> repository) : base(mapper, repository)
        {
        }

        protected override async Task<Entities.InventoryItems> GetWithIncludesAsync(Guid entityId)
        {
            return await Repository.GetAsync(entityId, nameof(Entities.InventoryItems.Inventory),
                nameof(Entities.InventoryItems.Item));
        }


        public async Task<InventoryItemsDto> GetAsync(Guid inventoryId, Guid itemId)
        {
            Expression<Func<Entities.InventoryItems, bool>> expr = a => (a.InventoryId==inventoryId && a.ItemId==itemId);
            var invItem = (await Repository.GetAllAsync(expr)).First();
            return Mapper.Map<InventoryItemsDto>(invItem);
        }
    }
}
