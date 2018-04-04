using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Witcherius.BL.DataTransferObjects.Item;
using Witcherius.BL.DataTransferObjects.Monster;
using Witcherius.BL.DataTransferObjects.Quest;
using Witcherius.BL.Facades.Common;
using Witcherius.BL.Services.Quest;
using Witcherius.Infrastructure.UnitOfWork;

namespace Witcherius.BL.Facades
{
    public class QuestFacade : BaseFacade
    {
        private readonly IQuestService _questService;
        public QuestFacade(IUnitOfWorkProvider unitOfWorkProvider, IQuestService questService) : base(unitOfWorkProvider)
        {
            _questService = questService;
        }

        public async Task<QuestDto> GetQuestAsync(Guid id)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await _questService.GetAsync(id);
            }
        }

        public async Task<Guid> CreateQuest(QuestDto quest)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                quest.Id = _questService.Create(quest);
                await uow.Commit();
                return quest.Id;
            }
        }

        public async Task<bool> EditQuestAsync(QuestEditDto quest)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                if ((await _questService.GetAsync(quest.Id, false)) == null)
                {
                    return false;
                }
                await _questService.Update(quest);
                await uow.Commit();
                return true;
            }
        }

        public async Task<bool> DeleteQuestAsync(Guid id)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                if ((await _questService.GetAsync(id, false)) == null)
                {
                    return false;
                }
                _questService.Delete(id);
                await uow.Commit();
                return true;
            }
        }

        public async Task<IEnumerable<QuestDto>> GetAllQuestsAsync()
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                return await _questService.ListAllAsync();
            }
        }

        public async Task<bool> AssignAsync(QuestDto quest, MonsterDto monster, ItemDto item)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                if ((await _questService.GetAsync(quest.Id, false)) == null)
                {
                    return false;
                }
                await _questService.Assign(quest, monster, item);
                await uow.Commit();
                return true;
            }
        }
    }
}
