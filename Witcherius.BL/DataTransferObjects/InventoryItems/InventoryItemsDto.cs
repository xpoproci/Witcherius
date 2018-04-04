using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Witcherius.BL.DataTransferObjects.Common;
using Witcherius.BL.DataTransferObjects.Inventory;
using Witcherius.BL.DataTransferObjects.Item;

namespace Witcherius.BL.DataTransferObjects.InventoryItems
{
    public class InventoryItemsDto : BaseDto
    {
        public Guid? ItemId { get; set; }
        public Guid? InventoryId { get; set; }

        public ItemDto Item { get; set; }
        public InventoryDto Inventory { get; set; }
    }
}
