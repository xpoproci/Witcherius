using System;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Threading.Tasks;
using AutoMapper;
using Witcherius.BL.DataTransferObjects.Character;
using Witcherius.BL.DataTransferObjects.Messages;
using Witcherius.BL.DataTransferObjects.User;
using Witcherius.BL.Services.Common;
using Witcherius.BL.Services.Message;
using Witcherius.Infrastructure;

namespace Witcherius.BL.Services.Message
{
    public class MessageService : CrudService<Entities.Message, MessageDto, MessageDto, MessageEditDto>, IMessageService
    {
        public MessageService(IMapper mapper, IRepository<Entities.Message> repository) : base(mapper, repository)
        {
        }

        protected override async Task<Entities.Message> GetWithIncludesAsync(Guid entityId)
        {
            return await Repository.GetAsync(entityId, nameof(Entities.Message.User));
        }
    }
}
