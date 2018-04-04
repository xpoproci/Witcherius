using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Witcherius.BL.DataTransferObjects.Common;

namespace Witcherius.BL.DataTransferObjects.Equipment
{
    public class EquipmentEditDto : BaseDto
    {
        public Guid? ArmorId { get; set; }
        public Guid? SwordId { get; set; }
        public Guid? TrousersId { get; set; }
    }
}
