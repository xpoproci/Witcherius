using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Witcherius.BL.DataTransferObjects.Item;
using Witcherius.BL.DataTransferObjects.Shop;
using Witcherius.BL.Services.Common;
using Witcherius.Infrastructure;

namespace Witcherius.BL.Services.Shop
{
    public class ShopService : CrudService<Entities.Shop, ShopDto, ShopDto, ShopEditDto>, IShopService
    {
        public ShopService(IMapper mapper, IRepository<Entities.Shop> repository) : base(mapper, repository)
        {
        }

        protected override async Task<Entities.Shop> GetWithIncludesAsync(Guid entityId)
        {
            return await Repository.GetAsync(entityId, nameof(Entities.Shop.Items));
        }
    }
}
