using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Witcherius.BL.DataTransferObjects.Common;

namespace Witcherius.BL.DataTransferObjects.Monster
{
    public class MonsterEditDto : BaseDto
    {
        public string Name { get; set; }
        public Guid? AttributesId { get; set; }
        public int Experience { get; set; }
        public string Image { get; set; }
    }
}
