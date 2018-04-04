using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Witcherius.BL.DataTransferObjects.InventoryItems;
using Witcherius.BL.DataTransferObjects.Item;

namespace Witcherius.BL.Services.InventoryItems
{
    public interface IInventoryItemsService
    {

        Task<InventoryItemsDto> GetAsync(Guid entityId, bool withIncludes = true);

        Task<InventoryItemsDto> GetAsync(Guid inventoryId, Guid itemId);
        
        Guid Create(InventoryItemsDto entityDto);

        Task Update(InventoryItemsDto entityDto);

        void Delete(Guid entityId);

        Task<IEnumerable<InventoryItemsDto>> ListAllAsync();
    }
}
