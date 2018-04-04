using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Witcherius.BL.DataTransferObjects.Item;
using Witcherius.BL.DataTransferObjects.Monster;
using Witcherius.BL.DataTransferObjects.Quest;

namespace Witcherius.BL.Services.Quest
{
    public interface IQuestService
    {
        Task<QuestDto> GetAsync(Guid entityId, bool withIncludes = true);

        Guid Create(QuestDto entityDto);

        Task Update(QuestEditDto entityDto);

        void Delete(Guid entityId);

        Task<IEnumerable<QuestDto>> ListAllAsync();

        Task Assign(QuestDto quest, MonsterDto monster, ItemDto item);
    }
}
