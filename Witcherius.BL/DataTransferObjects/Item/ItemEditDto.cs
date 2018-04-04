using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Witcherius.BL.DataTransferObjects.Common;
using Witcherius.BL.Enums;

namespace Witcherius.BL.DataTransferObjects.Item
{
    public class ItemEditDto : BaseDto
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public ArmorClass ArmorClass { get; set; }
        public Quality Quality { get; set; }
        public string Image { get; set; }

        public Guid? ShopId { get; set; }
    }
}
