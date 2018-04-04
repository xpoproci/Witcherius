using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Witcherius.BL.DataTransferObjects.Character;
using Witcherius.BL.DataTransferObjects.User;

namespace Witcherius.BL.Services.User
{
    public interface IUserService
    {
        /// <summary>
        /// Gets user with given name
        /// </summary>
        /// <param name="name">name</param>
        /// <returns>User with given name</returns>
        Task<UserDto> GetUserAccordingToUserName(string name);

        Task<UserDto> GetAsync(Guid entityId, bool withIncludes = true);

        Task Assign(UserDto user, CharacterCreateDto character);

        Guid Create(UserCreateDto entityDto);

        Task Update(UserEditDto entityDto);
        
        void Delete(Guid entityId);

        Task<Guid> RegisterUserAsync(UserCreateDto userDto);

        Task<Tuple<bool, string>> AuthorizeUserAsync(string username, string password);
        
        Task<IEnumerable<UserDto>> ListAllAsync();
    }
}