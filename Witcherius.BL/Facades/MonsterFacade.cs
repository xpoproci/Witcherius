using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Witcherius.BL.DataTransferObjects.Attributes;
using Witcherius.BL.DataTransferObjects.Monster;
using Witcherius.BL.Facades.Common;
using Witcherius.BL.Services.Attributes;
using Witcherius.BL.Services.Monster;
using Witcherius.Infrastructure.UnitOfWork;

namespace Witcherius.BL.Facades
{
    public class MonsterFacade : BaseFacade
    {
        private readonly IMonsterService _monsterService;
        private readonly IAttributesService _attributesService;

        public MonsterFacade(IUnitOfWorkProvider unitOfWorkProvider, IMonsterService monsterService, IAttributesService attributesService) : base(unitOfWorkProvider)
        {
            _monsterService = monsterService;
            _attributesService = attributesService;
        }

        #region CRUD
        public async Task<IEnumerable<MonsterDto>> GetAllMonstersAsync()
        {
            using (UnitOfWorkProvider.Create())
            {
                return await _monsterService.ListAllAsync();
            }
        }

        public async Task<MonsterDto> GetMonsterAsync(Guid id)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await _monsterService.GetAsync(id);
            }
        }

        public async Task<Guid> CreateMonster(MonsterDto monster, int exp = 1)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var attr = GenerateAttributesAccordingToExperience(exp);
                attr.Id = Guid.NewGuid();

                monster.Attributes = attr;
                monster.Id = _monsterService.Create(monster);
                await uow.Commit();
                return monster.Id;
            }
        }

        public async Task<Guid> CreateMonster(MonsterDto monster, AttributesDto attr)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                attr.Id = Guid.NewGuid();

                monster.Attributes = attr;
                monster.Id = _monsterService.Create(monster);
                await uow.Commit();
                return monster.Id;
            }
        }

        public async Task<bool> EditMonsterAsync(MonsterEditDto monster)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                if ((await _monsterService.GetAsync(monster.Id, false)) == null)
                {
                    return false;
                }
                await _monsterService.Update(monster);
                await uow.Commit();
                return true;
            }
        }

        public async Task<bool> DeleteMonsetrAsync(Guid id)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                if ((await _monsterService.GetAsync(id, false)) == null)
                {
                    return false;
                }
                _monsterService.Delete(id);
                await uow.Commit();
                return true;
            }
        }


        #endregion

        private AttributesDto GenerateAttributesAccordingToExperience(int ex)
        {
            var baseAttr = new AttributesDto() {Hp = 15, Damage = 15, Defense = 15};
            var lvl = Math.Floor(25 + Math.Sqrt(625 + 100 * ex)) / 50;
            baseAttr.Hp += (int) lvl;
            baseAttr.Defense += (int)lvl;
            baseAttr.Damage += (int)lvl;
            return baseAttr;
        }

    }
}
