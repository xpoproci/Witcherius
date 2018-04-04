using System;
using System.Collections.Generic;
using Witcherius.BL.DataTransferObjects.Common;
using Witcherius.BL.DataTransferObjects.Item;

namespace Witcherius.BL.DataTransferObjects.Equipment
{
    public class EquipmentDto : BaseDto
    {
        public ItemDto Sword { get; set; }
        public ItemDto Armor { get; set; }
        public ItemDto Trousers { get; set; }

        public Guid? ArmorId { get; set; }
        public Guid? SwordId { get; set; }
        public Guid? TrousersId { get; set; }

        public List<ItemDto> ListItems()
        {
            return new List<ItemDto>() {Sword, Armor, Trousers};
        }
    }
}
