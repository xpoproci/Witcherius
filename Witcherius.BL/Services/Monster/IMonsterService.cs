using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Witcherius.BL.DataTransferObjects.Monster;

namespace Witcherius.BL.Services.Monster
{
    public interface IMonsterService
    {

        Task<MonsterDto> GetAsync(Guid entityId, bool withIncludes = true);

        Guid Create(MonsterDto entityDto);

        Task Update(MonsterEditDto entityDto);

        void Delete(Guid entityId);

        Task<IEnumerable<MonsterDto>> ListAllAsync();

        Task<MonsterDto> GetByName(string name);
    }
}
