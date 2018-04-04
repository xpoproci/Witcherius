using System;
using Witcherius.BL.DataTransferObjects.Common;
using Witcherius.BL.DataTransferObjects.Item;
using Witcherius.BL.DataTransferObjects.Monster;

namespace Witcherius.BL.DataTransferObjects.Quest
{
    public class QuestDto : BaseDto
    {
        public string Name { get; set; }
        public int Experience { get; set; }
        public int Gold { get; set; }
        public MonsterDto Monster { get; set; }
        public ItemDto Item { get; set; }

        public Guid? MonsterId { get; set; }
        public Guid? ItemId { get; set; }
    }
}
