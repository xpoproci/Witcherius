using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Witcherius.Infrastructure;

namespace Witcherius.Entities
{
    public class InventoryItems : IEntity
    {
        public Guid? InventoryId { get; set; }
        public Guid? ItemId { get; set; }

        public virtual Inventory Inventory { get; set; }
        public virtual Item Item { get; set; }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }
    }
}
