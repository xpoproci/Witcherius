using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Witcherius.Infrastructure;

namespace Witcherius.Entities
{
    public class Equipment : IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }
        public virtual Item Sword { get; set; }
        public Guid? SwordId { get; set; }

        public virtual Item Armor { get; set; }
        public Guid? ArmorId { get; set; }

        public virtual Item Trousers { get; set; }
        public Guid? TrousersId { get; set; }
    }
}
