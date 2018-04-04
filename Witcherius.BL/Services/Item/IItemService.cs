using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Witcherius.BL.DataTransferObjects.Item;

namespace Witcherius.BL.Services.Item
{
    public interface IItemService
    {

        Task<ItemDto> GetAsync(Guid entityId, bool withIncludes = true);
        
        Guid Create(ItemDto entityDto);

        Task Update(ItemEditDto entityDto);

        void Delete(Guid entityId);

        Task<IEnumerable<ItemDto>> ListAllAsync();

        Task<IEnumerable<ItemDto>> ListAllForRequiredLevel(int level);
    }
}
