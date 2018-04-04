using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Witcherius.BL.DataTransferObjects.Character;
using Witcherius.BL.DataTransferObjects.Messages;
using Witcherius.BL.DataTransferObjects.User;
using Witcherius.BL.Facades.Common;
using Witcherius.BL.Services.Message;
using Witcherius.BL.Services.User;
using Witcherius.Infrastructure.UnitOfWork;

namespace Witcherius.BL.Facades
{
    public class UserFacade : BaseFacade
    {
        private readonly IUserService _service;
        private readonly IMessageService _messageService;

        public UserFacade(IUnitOfWorkProvider unitOfWorkProvider, IUserService service, IMessageService messageService) : base(unitOfWorkProvider)
        {
            _service = service;
            _messageService = messageService;
        }

        public async Task<MessageDto> MessageGetAsync(Guid entityId)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await _messageService.GetAsync(entityId);
            }
        }

        public async Task<Guid> MessageCreateAsync(UserDto user, MessageDto message)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                message.UserId = user.Id;
                message.Id = _messageService.Create(message);
                await uow.Commit();
                return message.Id;
            }
        }

        public async Task<bool> MessageEditAsync(MessageEditDto message)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                if ((await _messageService.GetAsync(message.Id)) == null) return false;
                await _messageService.Update(message);
                await uow.Commit();
                return true;
            }
        }

        public async Task<bool> AssignAsync(UserDto user, CharacterCreateDto character)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                if ((await _service.GetAsync(user.Id, false)) == null)
                {
                    return false;
                }
                await _service.Assign(user, character);
                await uow.Commit();
                return true;
            }
        }

        public async Task<UserDto> GetAsync(Guid entityId)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await _service.GetAsync(entityId);
            }
        }

        public async Task<Guid> CreateAsync(UserCreateDto entityDto)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                entityDto.Id = _service.Create(entityDto);
                await uow.Commit();
                return entityDto.Id;
            }
        }

        public async Task<bool> EditAsync(UserEditDto entityDto)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                if ((await _service.GetAsync(entityDto.Id, false)) == null)
                {
                    return false;
                }
                await _service.Update(entityDto);
                await uow.Commit();
                return true;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                if ((await _service.GetAsync(id, false)) == null)
                {
                    return false;
                }
                _service.Delete(id);
                await uow.Commit();
                return true;
            }
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            using (UnitOfWorkProvider.Create())
            {
                return await _service.ListAllAsync();
            }
        }

        public async Task<Guid> RegisterUser(UserCreateDto userCreateDto)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var id = await _service.RegisterUserAsync(userCreateDto);
                await uow.Commit();
                return id;
            }
        }

        public async Task<Tuple<bool, string>> Login(string username, string password)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await _service.AuthorizeUserAsync(username, password);
            }
        }

        public async Task<UserDto> GetUserAccordingToUsernameAsync(string username)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await _service.GetUserAccordingToUserName(username);
            }
        }

    }
}
