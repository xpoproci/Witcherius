using System;
using System.Threading.Tasks;
using AutoMapper;
using Witcherius.BL.DataTransferObjects.Item;
using Witcherius.BL.DataTransferObjects.Monster;
using Witcherius.BL.DataTransferObjects.Quest;
using Witcherius.BL.Services.Common;
using Witcherius.Infrastructure;

namespace Witcherius.BL.Services.Quest
{
    public class QuestService : CrudService<Entities.Quest, QuestDto, QuestDto, QuestEditDto>, IQuestService
    {

        public QuestService(IMapper mapper, IRepository<Entities.Quest> repository) : base(mapper, repository)
        {
        }

        protected override async Task<Entities.Quest> GetWithIncludesAsync(Guid entityId)
        {
            return await Repository.GetAsync(entityId);
        }

        public async Task Assign(QuestDto quest, MonsterDto monster, ItemDto item)
        {
            var q = Mapper.Map<QuestEditDto>(quest);
            q.MonsterId = monster.Id;
            q.ItemId = item.Id;
            await Update(q);
        }
    }
}
