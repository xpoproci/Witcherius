using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Witcherius.BL.DataTransferObjects.Common;

namespace Witcherius.BL.DataTransferObjects.MiniLocation
{
    public class MiniLocationEditDto : BaseDto
    {
        public string Name { get; set; }
        public int MinimumLevel { get; set; }
        public Guid? QuestId { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public Guid? LocationId { get; set; }
    }
}
