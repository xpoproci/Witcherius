using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Witcherius.BL.DataTransferObjects.Attributes;

namespace Witcherius.BL.DataTransferObjects.Combat
{
    public class CombatDto
    {
        public AttributesDto Attr { get; set; }
        public int Hp { get; set; }
        public bool First { get; set; }

        public CombatDto(AttributesDto a, bool f = false)
        {
            Attr = a;
            Hp = a.Hp * 10;
            First = f;
        }
    }
}
