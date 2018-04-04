using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Witcherius.BL.DataTransferObjects.Character;
using Witcherius.BL.DataTransferObjects.Shop;

namespace Witcherius.PL.Models
{
    public class ShopCharacterModel
    {
        public ShopDto Shop { get; set; }
        public CharacterDto Character { get; set; }
    }
}