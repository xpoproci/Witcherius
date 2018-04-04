using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Witcherius.BL.DataTransferObjects.Common;
using Witcherius.BL.Enums;

namespace Witcherius.BL.DataTransferObjects.Shop
{
    public class ShopEditDto : BaseDto
    {
        public string Name { get; set; }
        public ArmorClass ArmorClass { get; set; }
    }
}
