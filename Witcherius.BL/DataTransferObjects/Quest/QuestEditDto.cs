using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Witcherius.BL.DataTransferObjects.Common;
using Witcherius.BL.DataTransferObjects.Item;
using Witcherius.BL.DataTransferObjects.Monster;

namespace Witcherius.BL.DataTransferObjects.Quest
{
    public class QuestEditDto:BaseDto
    {
        public string Name { get; set; }
        public int Experience { get; set; }
        public int Gold { get; set; }
        public Guid? MonsterId { get; set; }
        public Guid? ItemId { get; set; }
    }
}
