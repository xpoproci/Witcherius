using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Witcherius.BL.DataTransferObjects.Item;

namespace Witcherius.PL.Models.Admin
{
    public class ShopEditModel
    {
        public Guid Id { get; set; }
        public int Minimum { get; set; }
        public int Maximum { get; set; }
        public IEnumerable<ItemDto> Items { get; set; }


        public ShopEditModel()
        {
            Items=new List<ItemDto>();
        }
    }
}