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
    public class Message: IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }
        public virtual User User { get; set; }
        public Guid? UserId { get; set; }
        public string MessageText { get; set; }
        public virtual DateTime Arrived { get; set; }
        public bool IsRead { get; set; } = false;
    }
}
