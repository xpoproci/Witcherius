using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Witcherius.BL.DataTransferObjects.Character;
using Witcherius.BL.DataTransferObjects.Messages;
using Witcherius.BL.DataTransferObjects.User;

namespace Witcherius.BL.Services.Message
{
    public interface IMessageService
    {
        Task<MessageDto> GetAsync(Guid entityId, bool withIncludes = true);
        
        Guid Create(MessageDto entityDto);

        Task Update(MessageEditDto entityDto);
        
        void Delete(Guid entityId);
        
        Task<IEnumerable<MessageDto>> ListAllAsync();
    }
}