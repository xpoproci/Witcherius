﻿using System.ComponentModel.DataAnnotations;

namespace Witcherius.PL.Models
{
    public class LoginModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}