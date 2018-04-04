using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Witcherius.BL.DataTransferObjects.Attributes;
using Witcherius.BL.DataTransferObjects.Equipment;
using Witcherius.BL.DataTransferObjects.Inventory;
using Witcherius.BL.DataTransferObjects.InventoryItems;
using Witcherius.BL.DataTransferObjects.Item;
using Witcherius.BL.Facades.Common;
using Witcherius.BL.Services.Equipment;
using Witcherius.BL.Services.InventoryItems;
using Witcherius.BL.Services.Item;
using Witcherius.Infrastructure.UnitOfWork;

namespace Witcherius.BL.Facades
{
    public class EquipmentFacade : BaseFacade
    {
        private readonly IEquipmentService _equipmentService;
        private readonly IItemService _itemService;
        private readonly IInventoryItemsService _inventoryItemsService;

        public EquipmentFacade(IUnitOfWorkProvider unitOfWorkProvider, IInventoryItemsService inventoryItemsService, IEquipmentService equipmentService, IItemService itemService) : base(unitOfWorkProvider)
        {
            _equipmentService = equipmentService;
            _itemService = itemService;
            _inventoryItemsService = inventoryItemsService;
        }

        public async Task<IEnumerable<ItemDto>> GetAllItemsAccordingToLevelAsync(int level)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await _itemService.ListAllForRequiredLevel(level);
            }
        }

        public async Task<ItemDto> GetItemAsync(Guid id)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await _itemService.GetAsync(id);
            }
        }

        public async Task<bool> UpdateItemAsync(ItemEditDto item)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                if ((await _itemService.GetAsync(item.Id, false)) == null)
                {
                    return false;
                }
                await _itemService.Update(item);
                await uow.Commit();
                return true;
            }
        }

        public async Task DeleteItemAsync(Guid id)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                _itemService.Delete(id);
                await uow.Commit();
            }
        }

        public async Task<IEnumerable<ItemDto>> GetAllItemsAsync()
        {
            using (UnitOfWorkProvider.Create())
            {
                return await _itemService.ListAllAsync();
            }
        }

        public async Task<IEnumerable<InventoryItemsDto>> GetAllItemsInInventories()
        {
            using (UnitOfWorkProvider.Create())
            {
                return await _inventoryItemsService.ListAllAsync();
            }
        }

        public async Task<bool> AddToInventoryAsync(InventoryDto inventory, ItemDto item)
        {
            if (inventory.IsFull)
            {
                return false;
            }

            using (var uow = UnitOfWorkProvider.Create())
            {
                var invItem = new InventoryItemsDto() {InventoryId = inventory.Id, ItemId = item.Id};
                invItem.Id = _inventoryItemsService.Create(invItem);
                await uow.Commit();
                return true;
            }
        }

        public async Task<bool> DeleteFromInventoryAsync(InventoryDto inventory, ItemDto item)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var invItem = await _inventoryItemsService.GetAsync(inventory.Id, item.Id);
                _inventoryItemsService.Delete(invItem.Id);
                await uow.Commit();
                return true;
            }
        }

        public async Task<Guid> CreateItem(ItemDto item, AttributesDto attr)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                attr.Id = Guid.NewGuid();
                item.Attributes= attr;
                item.Id = _itemService.Create(item);
                await uow.Commit();
                return item.Id;
            }
        }

        public async Task<bool> EquipItemAsync(EquipmentDto eq, ItemDto item)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                if ((await _equipmentService.GetAsync(eq.Id, false)) == null ||
                    (await _itemService.GetAsync(item.Id, false)) == null)
                {
                    return false;
                }
                await _equipmentService.EquipItemAsync(eq, item);
                await uow.Commit();
                return true;
            }
        }

        public async Task<EquipmentDto> GetEquipmentAsync(Guid id)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await _equipmentService.GetAsync(id);
            }
        }

        public async Task<Guid> CreateEquipment(EquipmentDto eq)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                eq.Id = _equipmentService.Create(eq);
                await uow.Commit();
                return eq.Id;
            }
        }

        public async Task<bool> EditEquipmentAsync(EquipmentEditDto eq)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                if ((await _equipmentService.GetAsync(eq.Id, false)) == null)
                {
                    return false;
                }
                await _equipmentService.Update(eq);
                await uow.Commit();
                return true;
            }
        }

        public async Task<bool> DeleteEquipmentAsync(Guid id)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                if ((await _equipmentService.GetAsync(id, false)) == null)
                {
                    return false;
                }
                _equipmentService.Delete(id);
                await uow.Commit();
                return true;
            }
        }
    }
}
