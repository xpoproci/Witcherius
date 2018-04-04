using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Witcherius.BL.DataTransferObjects.Inventory;

namespace Witcherius.BL.Services.Inventory
{
    public interface IInventoryService
    {
        Task<InventoryDto> GetAsync(Guid entityId, bool withIncludes = true);
        
        Guid Create(InventoryDto entityDto);

        Task Update(InventoryEditDto entityDto);

        void Delete(Guid entityId);

        Task<IEnumerable<InventoryDto>> ListAllAsync();
    }
}