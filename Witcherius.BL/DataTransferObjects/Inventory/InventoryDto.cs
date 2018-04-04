using System.Collections.Generic;
using System.Linq;
using Witcherius.BL.DataTransferObjects.Common;
using Witcherius.BL.DataTransferObjects.InventoryItems;
using Witcherius.BL.DataTransferObjects.Item;

namespace Witcherius.BL.DataTransferObjects.Inventory
{
    public class InventoryDto : BaseDto
    {
        public ICollection<InventoryItemsDto> Items { get; set; }
        public int MaxSize { get; set; }
        public bool IsFull => Items?.Count() >= MaxSize;

        public InventoryDto()
        {
            Items = new List<InventoryItemsDto>();
        }
    }
}
