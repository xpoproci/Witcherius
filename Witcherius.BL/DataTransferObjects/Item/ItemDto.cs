using System;
using System.Collections.Generic;
using Witcherius.BL.DataTransferObjects.Attributes;
using Witcherius.BL.DataTransferObjects.Common;
using Witcherius.BL.DataTransferObjects.Inventory;
using Witcherius.BL.DataTransferObjects.InventoryItems;
using Witcherius.BL.Enums;

namespace Witcherius.BL.DataTransferObjects.Item
{
    public class ItemDto : BaseDto
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public ArmorClass ArmorClass { get; set; }
        public Quality Quality { get; set; }
        public int RequiredLevel { get; set; }
        public string Image { get; set; }
        public AttributesDto Attributes { get; set; }
        public Guid? AttributesId { get; set; }
        public ICollection<InventoryItemsDto> Inventories { get; set; }
        
        public Guid? ShopId { get; set; }

        public ItemDto()
        {
            Inventories = new List<InventoryItemsDto>();
        }

        public override string ToString()
        {
            return $"{ArmorClass}_{Name}";
        }
    }
}
