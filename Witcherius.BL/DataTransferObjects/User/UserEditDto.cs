using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Witcherius.BL.DataTransferObjects.Common;

namespace Witcherius.BL.DataTransferObjects.User
{
    public class UserEditDto : BaseDto
    {
        public int Credits { get; set; }
        public int Score { get; set; }
        public Guid? CharacterId { get; set; }
    }
}
