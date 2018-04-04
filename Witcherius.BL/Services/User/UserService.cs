using System;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Threading.Tasks;
using AutoMapper;
using Witcherius.BL.DataTransferObjects.Character;
using Witcherius.BL.DataTransferObjects.User;
using Witcherius.BL.Services.Common;
using Witcherius.Infrastructure;

namespace Witcherius.BL.Services.User
{
    public class UserService : CrudService<Entities.User, UserDto, UserCreateDto, UserEditDto>, IUserService
    {
        private readonly IRepository<Entities.Character> _characterRepository;
        private const int PBKDF2IterCount = 100000;
        private const int PBKDF2SubkeyLength = 160 / 8;
        private const int saltSize = 128 / 8;

        public UserService(IMapper mapper, IRepository<Entities.User> repository, IRepository<Entities.Character> characterRepository) : base(mapper, repository)
        {
            _characterRepository = characterRepository;
        }

        protected override async Task<Entities.User> GetWithIncludesAsync(Guid entityId)
        {
            return await Repository.GetAsync(entityId, nameof(Entities.User.Character));
        }

        public async Task<UserDto> GetUserAccordingToUserName(string name)
        {
            Expression<Func<Entities.User, bool>> expr = a => a.Username.Equals(name);
            var user = (await ListAllAsync(expr)).FirstOrDefault();
            return Mapper.Map<UserDto>(user);
        }

        public async Task Assign(UserDto user, CharacterCreateDto character)
        {
            var u = Mapper.Map<UserEditDto>(user);
            u.CharacterId = character.Id;
            await Update(u);
        }

        #region AuthorizationAndRegistration

        public async Task<Guid> RegisterUserAsync(UserCreateDto userDto)
        {
            var user = Mapper.Map<Entities.User>(userDto);

            if (await GetIfUserExistsAsync(user.Username))
            {
                throw new ArgumentException();
            }

            var password = CreateHash(userDto.Password);
            user.PasswordHash = password.Item1;
            user.PasswordSalt = password.Item2;

            Repository.Create(user);

            return user.Id;
        }

        public async Task<Tuple<bool, string>> AuthorizeUserAsync(string username, string password)
        {
            var user = await GetUserAccordingToUserName(username);

            var succ = user != null && VerifyHashedPassword(user.PasswordHash, user.PasswordSalt, password);
            var roles = user?.Roles ?? "";
            return new Tuple<bool, string>(succ, roles);
        }

        private async Task<bool> GetIfUserExistsAsync(string username)
        {
            Expression<Func<Entities.User, bool>> expr = a => a.Username.Equals(username);
            return (await ListAllAsync(expr)).Count() == 1;
        }

        private static bool VerifyHashedPassword(string hashedPassword, string salt, string password)
        {
            var hashedPasswordBytes = Convert.FromBase64String(hashedPassword);
            var saltBytes = Convert.FromBase64String(salt);

            using (var deriveBytes = new Rfc2898DeriveBytes(password, saltBytes, PBKDF2IterCount))
            {
                var generatedSubkey = deriveBytes.GetBytes(PBKDF2SubkeyLength);
                return hashedPasswordBytes.SequenceEqual(generatedSubkey);
            }
        }

        private static Tuple<string, string> CreateHash(string password)
        {
            using (var deriveBytes = new Rfc2898DeriveBytes(password, saltSize, PBKDF2IterCount))
            {
                var salt = deriveBytes.Salt;
                var subkey = deriveBytes.GetBytes(PBKDF2SubkeyLength);

                return Tuple.Create(Convert.ToBase64String(subkey), Convert.ToBase64String(salt));
            }
        }

        #endregion

    }
}
