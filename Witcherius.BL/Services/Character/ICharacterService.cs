using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Witcherius.BL.DataTransferObjects.Character;

namespace Witcherius.BL.Services.Character
{
    public interface ICharacterService
    {
        Task<CharacterDto> GetAsync(Guid entityId, bool withIncludes = true);

        Guid Create(CharacterCreateDto entityDto);
        
        Task Update(CharacterUpdateDto entityDto);

        void Delete(Guid entityId);

        Task<IEnumerable<CharacterDto>> ListAllAsync();
    }
}