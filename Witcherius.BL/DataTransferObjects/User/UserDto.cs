using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Witcherius.BL.DataTransferObjects.Character;
using Witcherius.BL.DataTransferObjects.Common;
using Witcherius.BL.DataTransferObjects.Messages;

namespace Witcherius.BL.DataTransferObjects.User
{
    public class UserDto : BaseDto
    {
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
        public CharacterDto Character { get; set; }

        public Guid? CharacterId { get; set; }

        public ICollection<MessageDto> Messages { get; set; }

    }
}
