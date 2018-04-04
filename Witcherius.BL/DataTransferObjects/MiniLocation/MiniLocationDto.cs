using System;
using Witcherius.BL.DataTransferObjects.Common;
using Witcherius.BL.DataTransferObjects.Quest;

namespace Witcherius.BL.DataTransferObjects.MiniLocation
{
    public class MiniLocationDto : BaseDto
    {
        public string Name { get; set; }
        public int MinimumLevel { get; set; }
        public QuestDto Quest { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public Guid? QuestId { get; set; }
        public Guid? LocationId { get; set; }

        public override string ToString()
        {
            return "mini_" + Name;
        }
    }
}
