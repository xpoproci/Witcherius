using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Witcherius.Enums;
using Witcherius.Infrastructure;

namespace Witcherius.Entities
{
    public class Shop : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ArmorClass ArmorClass { get; set; }
        public virtual ICollection<Item> Items { get; set; }

        public Shop()
        {
            Items = new List<Item>();
        }
    }
}
