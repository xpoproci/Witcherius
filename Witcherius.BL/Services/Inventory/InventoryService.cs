using System;
using System.Threading.Tasks;
using AutoMapper;
using Witcherius.BL.DataTransferObjects.Inventory;
using Witcherius.BL.Services.Common;
using Witcherius.Infrastructure;

namespace Witcherius.BL.Services.Inventory
{
    public class InventoryService : CrudService<Entities.Inventory, InventoryDto, InventoryDto, InventoryEditDto>, IInventoryService
    {
        public InventoryService(IMapper mapper, IRepository<Entities.Inventory> repository) : base(mapper, repository)
        {
        }

        protected override async Task<Entities.Inventory> GetWithIncludesAsync(Guid entityId)
        {
            return await Repository.GetAsync(entityId, nameof(Entities.Inventory.Items));
        }

    }
}
