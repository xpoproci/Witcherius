using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Witcherius.BL.DataTransferObjects.Character;
using Witcherius.BL.DataTransferObjects.User;

namespace Witcherius.PL.Models
{
    public class CombatModel
    {
        public UserDto Player1 { get; set; }
        public UserDto Player2 { get; set; }
    }
}