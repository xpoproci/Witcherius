using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Witcherius.BL.DataTransferObjects.Attributes;
using Witcherius.BL.DataTransferObjects.Item;
using Witcherius.BL.DataTransferObjects.Shop;
using Witcherius.BL.Enums;
using Witcherius.BL.Facades.Common;
using Witcherius.BL.Services.Item;
using Witcherius.BL.Services.Shop;
using Witcherius.Infrastructure.UnitOfWork;

namespace Witcherius.BL.Facades
{
    public class ShopFacade : BaseFacade
    {
        private readonly IShopService _shopService;
        private readonly IItemService _itemService;
        private Random _random;

        public ShopFacade(IUnitOfWorkProvider unitOfWorkProvider, IShopService shopService, IItemService itemService) : base(unitOfWorkProvider)
        {
            _shopService = shopService;
            _itemService = itemService;
        }

        #region CRUD
        public async Task<ShopDto> GetAsync(Guid id)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await _shopService.GetAsync(id);
            }
        }

        public async Task<Guid> CreateAsync(ShopDto shop)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                shop.Id = _shopService.Create(shop);
                await uow.Commit();
                return shop.Id;
            }
        }

        public async Task<bool> EditAsync(ShopEditDto shop)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                if ((await _shopService.GetAsync(shop.Id, false)) == null)
                {
                    return false;
                }
                await _shopService.Update(shop);
                await uow.Commit();
                return true;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                if ((await _shopService.GetAsync(id, false)) == null)
                {
                    return false;
                }
                _shopService.Delete(id);
                await uow.Commit();
                return true;
            }
        }
        #endregion


        public async Task AddItemToShopAsync(ShopDto shop, ItemEditDto item)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                item.ShopId = shop.Id;
                await _itemService.Update(item);
                await uow.Commit();
            }
        }

        public async Task DeleteItemFromShopAsync(ItemEditDto item)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                item.ShopId = null;
                await _itemService.Update(item);
                await uow.Commit();
            }
        }
        /*
        public async Task RenewShopItemsAsync(ShopDto shop, IEnumerable<ItemEditDto> items)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                foreach (var item in items)
                {
                    item.ShopId = shop.Id;
                    await _itemService.Update(item);
                }
                await uow.Commit();
            }
        }
        */
        public async Task<IEnumerable<ShopDto>> GetAllAsync()
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                return await _shopService.ListAllAsync();
            }
        }

        public async Task GenerateItemsAsync(ShopDto shop, int minLevel, int maxLevel)
        {
            _random = new Random();
            using (var uow = UnitOfWorkProvider.Create())
            {
                for (var i = minLevel; i <= maxLevel; i++)
                {
                    for (var j = 0; j < 4; j++)
                    {
                        var result = GenerateItemBasedOnLevel(i, shop.ArmorClass);
                        var item = result.Item1;
                        var attr = result.Item2;
                        attr.Id = Guid.NewGuid();
                        item.ShopId = shop.Id;
                        item.Attributes = attr;
                        item.Id = _itemService.Create(item);
                    }                    
                }
                await uow.Commit();
            }
        }

        private Tuple<ItemDto, AttributesDto> GenerateItemBasedOnLevel(int level, ArmorClass armorClass)
        {
            var quality = _random.Next(0, 100);
            var item = new ItemDto() {Price = level*15,ArmorClass = armorClass, RequiredLevel = level, Quality = ItemQuality(quality), Name = ItemName(armorClass)};
            item.Image = (armorClass.ToString().ToLower() + "_" + item.Quality.ToString().ToLower() + ".jpg");            

            var attr = ItemAttributes(level, armorClass, item.Quality);
            return new Tuple<ItemDto, AttributesDto>(item, attr);

        }

        //random attributes
        private AttributesDto ItemAttributes(int level, ArmorClass armor, Quality quality)
        {
            var multiplier = 1.0;
            switch (quality)
            {
                case Quality.Common: break;
                case Quality.Rare:
                    multiplier = 1.4;
                    break;
                case Quality.Epic:
                    multiplier = 1.7;
                    break;
                case Quality.Legendary: break;
            }

            var attr = new AttributesDto();

            switch (armor)
            {
                case ArmorClass.Sword:
                    attr.Hp = (int)(level*multiplier);
                    attr.Damage = 2* (int)(level * multiplier);
                    attr.Defense = (int)(level * multiplier);
                    return attr;
                case ArmorClass.Armor:
                    attr.Hp = 2 * (int)(level * multiplier);
                    attr.Damage = (int)(level * multiplier);
                    attr.Defense = (int)(level * multiplier);
                    return attr;
                case ArmorClass.Trousers:
                    attr.Hp = (int)(level * multiplier);
                    attr.Damage = (int)(level * multiplier);
                    attr.Defense = 2* (int)(level * multiplier);
                    return attr;
                case ArmorClass.Consumable:
                    attr.Hp = 5 * (int)(level * multiplier);
                    attr.Damage = 0;
                    attr.Defense = 0;
                    return attr;
                default:
                    return attr;
            }
        }

        //quality of item --cant be legendary
        private Quality ItemQuality(int i)
        {
            if (i > 75 && i < 98)
            {
                return Quality.Rare;
            }
            return i >= 98 ? Quality.Epic : Quality.Common;
        }

        //random item name
        private string ItemName(ArmorClass armorClass)
        {
            var additives = new[] {"Iron", "Bronze", "Silver", "Steel", "Golden", "Leather", "Cloth" };
            string[] names;
            string full;

            switch (armorClass)
            {
                case ArmorClass.Sword:
                    names = new[]{"Cleaver", "Sword", "Rapier", "Scimitar", "Katana", "Dagger"};
                    full = additives[_random.Next(additives.Length)] + " " +
                               names[_random.Next(names.Length)];
                    return full;
                case ArmorClass.Armor:
                    names = new[] { "Torso", "Chestplate", "Jacket", "Deffender", "Spiked Armor" };
                    full = additives[_random.Next(additives.Length)] + " " +
                               names[_random.Next(names.Length)];
                    return full;
                case ArmorClass.Trousers:
                    names = new[] { "Legs", "Skirt", "Trousers", "Spiked Legs", "Trimmed Skirt" };
                    full = additives[_random.Next(additives.Length)] + " " +
                           names[_random.Next(names.Length)];
                    return full;
                case ArmorClass.Consumable:
                    names = new[] { "Potion", "Chicken", "Raw Beef", "Roasted Chicken", "Bread", "Spicy Juice" };
                    full = names[_random.Next(names.Length)];
                    return full;
                default:
                    return "";
            }
        }

    }
}
