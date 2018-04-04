using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Witcherius.Infrastructure;

namespace Witcherius.Entities
{
    public class Quest : IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Experience { get; set; }
        public int Gold { get; set; }

        //navigation properties
        public virtual Monster Monster { get; set; }
        public Guid? MonsterId { get; set; }

        public virtual Item Item { get; set; }
        public Guid? ItemId { get; set; }
    }
}
