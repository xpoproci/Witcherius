using Witcherius.BL.DataTransferObjects.Common;

namespace Witcherius.BL.DataTransferObjects.Attributes
{
    public class AttributesDto : BaseDto
    {
        public int Hp { get; set; }
        public int Damage { get; set; }
        public int Defense { get; set; }
    }
}
