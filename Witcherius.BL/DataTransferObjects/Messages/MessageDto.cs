using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Witcherius.BL.DataTransferObjects.Common;
using Witcherius.BL.DataTransferObjects.User;

namespace Witcherius.BL.DataTransferObjects.Messages
{
    public class MessageDto : BaseDto
    {
        public UserDto User { get; set; }
        public Guid? UserId { get; set; }
        public string MessageText { get; set; }
        public DateTime Arrived { get; set; }
        public bool IsRead { get; set; }
    }
}
