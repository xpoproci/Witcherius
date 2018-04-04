using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Witcherius.Infrastructure;

namespace Witcherius.Entities
{
    public class MiniLocation : IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

        //navigation properties
        public virtual Quest Quest { get; set; }
        public Guid? QuestId{get; set; }
        public Guid? LocationId { get; set; }
    }
}
