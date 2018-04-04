using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Witcherius.BL.DataTransferObjects.Item;
using Witcherius.BL.DataTransferObjects.Shop;

namespace Witcherius.BL.Services.Shop
{
    public interface IShopService
    {
        Task<ShopDto> GetAsync(Guid entityId, bool withIncludes = true);
        
        Guid Create(ShopDto entityDto);

        Task Update(ShopEditDto entityDto);

        void Delete(Guid entityId);
        
        Task<IEnumerable<ShopDto>> ListAllAsync();
    }
}
