using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Witcherius.Infrastructure;

namespace Witcherius.Entities
{
    public class Inventory : IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }
        public virtual ICollection<InventoryItems> Items { get; set; }
        public int MaxSize { get; set; }

        public Inventory()
        {
            Items = new List<InventoryItems>();
        }
    }
}
