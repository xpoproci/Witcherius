using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Witcherius.BL.DataTransferObjects.Common;

namespace Witcherius.BL.DataTransferObjects.Inventory
{
    public class InventoryEditDto : BaseDto
    {
        public int MaxSize { get; set; }
    }
}
