using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Witcherius.Infrastructure;

namespace Witcherius.Entities
{
    public class Location : IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<MiniLocation> MiniLocation { get; set; }
        public string Description { get; set; }
        public int RequiredLevel { get; set; }
        public string Image { get; set; }

        public Location()
        {
            MiniLocation = new List<MiniLocation>();
        }
    }
}
