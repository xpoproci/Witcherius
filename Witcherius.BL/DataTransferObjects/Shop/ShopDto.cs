using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Witcherius.BL.DataTransferObjects.Common;
using Witcherius.BL.DataTransferObjects.Item;
using Witcherius.BL.Enums;

namespace Witcherius.BL.DataTransferObjects.Shop
{
    public class ShopDto : BaseDto
    {
        public string Name { get; set; }
        public ArmorClass ArmorClass { get; set; }
        public virtual ICollection<ItemDto> Items { get; set; }

        public ShopDto()
        {
            Items = new List<ItemDto>();
        }
    }
}
