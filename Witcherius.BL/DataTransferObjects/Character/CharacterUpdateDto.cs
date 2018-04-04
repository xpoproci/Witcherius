using System;
using Witcherius.BL.DataTransferObjects.Common;

namespace Witcherius.BL.DataTransferObjects.Character
{
    public class CharacterUpdateDto : BaseDto
    {
        public int Experience { get; set; }
        public int CurrentHp { get; set; }
        public int Gold { get; set; }
        public string Name { get; set; }
        public int SkillPoints { get; set; }

        public DateTime? Sickness { get; set; }
    }
}
