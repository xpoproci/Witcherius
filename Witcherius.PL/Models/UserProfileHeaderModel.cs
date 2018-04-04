using System.Linq;
using Witcherius.BL.DataTransferObjects.User;
using Witcherius.BL.Enums;

namespace Witcherius.PL.Models
{
    public class UserProfileHeaderModel
    {
        public string Username { get; set; }
        public string Email { get; set; }

        public School School { get; set; }
        public Race Race { get; set; }

        public int Gold { get; set; } = 0;
        public int Level { get; set; } = 1;
        public string CharName { get; set; } = "";
        public int MessageCount { get; set; }

        private readonly UserDto _user;

        public UserProfileHeaderModel(UserDto u)
        {
            _user = u;
            Username = u.Username;
            Email = u.Email;
            if (u.Character == null) return;
            CharName = u.Character.Name;
            School = u.Character.School;
            Race = u.Character.Race;
            Gold = u.Character.Gold;
            Level = u.Character.Level;
            MessageCount = u.Messages.Count(a=>!a.IsRead);
        }
       
        public string Avatar()
        {
            return _user.Character == null ? $"char_unknown.jpg" : $"{Race.ToString().ToLower()}_{School.ToString().ToLower()}.jpg";
        }
    }
}