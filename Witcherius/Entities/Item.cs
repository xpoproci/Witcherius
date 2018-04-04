using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Witcherius.Enums;
using Witcherius.Infrastructure;

namespace Witcherius.Entities
{
    public class Item : IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public ArmorClass ArmorClass { get; set; }
        public Quality Quality { get; set; }
        public string Image { get; set; }
        public int RequiredLevel { get; set; }

        public virtual ICollection<InventoryItems> Inventories { get; set; }

        //navigation properties
        public virtual Attributes Attributes { get; set; }
        public Guid? AttributesId { get; set; }

        public virtual Shop Shop { get; set; }
        public Guid? ShopId { get; set; }

        public Item()
        {
            Inventories = new List<InventoryItems>();
        }
    }
}
