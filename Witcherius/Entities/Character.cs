using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Witcherius.Enums;
using Witcherius.Infrastructure;

namespace Witcherius.Entities
{
    public class Character : IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }
        public int Gold { get; set; }
        public string Name { get; set; }
        public int CurrentHp { get; set; }
        public int Experience { get; set; } = 1;
        public int SkillPoints { get; set; } = 0;
        public School School { get; set; }
        public Race Race { get; set; }
        public virtual DateTime? Sickness { get; set; }
        
        //navigation properties
        public virtual Attributes Attributes { get; set; }
        public Guid? AttributesId { get; set; }

        public virtual Equipment Equipment { get; set; }
        public Guid? EquipmentId { get; set; }

        public virtual Inventory Inventory { get; set; }
        public Guid? InventoryId { get; set; }
    }
}
