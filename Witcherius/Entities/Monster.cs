using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Witcherius.Infrastructure;

namespace Witcherius.Entities
{
    public class Monster : IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Experience { get; set; }
        public string Image { get; set; }

        //navigation properties
        public virtual Attributes Attributes { get; set; }
        public Guid? AttributesId { get; set; }
    }
}
