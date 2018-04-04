using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Witcherius.Infrastructure;

namespace Witcherius.Entities
{
    public class User : IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }
        [Required]
        public string Username { get; set; }

        [Required, StringLength(100)]
        public string PasswordHash { get; set; }

        [Required, StringLength(100)]
        public string PasswordSalt { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string Roles { get; set; }

        public int Credits { get; set; }
        public int Score { get; set; }

        //navigation properties
        public virtual Character Character { get; set; }
        public Guid? CharacterId { get; set; }

        public virtual ICollection<Message> Messages { get; set; }

        public User()
        {
            Messages = new List<Message>();
        }
    }
}
