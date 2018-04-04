using Witcherius.BL.DataTransferObjects.Attributes;
using Witcherius.BL.DataTransferObjects.Common;

namespace Witcherius.BL.DataTransferObjects.Monster
{
    public class MonsterDto : BaseDto
    {
        public string Name { get; set; }
        public AttributesDto Attributes { get; set; }
        public int Experience { get; set; }
        public string Image { get; set; }
    }
}
