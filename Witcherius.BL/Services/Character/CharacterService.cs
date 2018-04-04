using System;
using System.Threading.Tasks;
using AutoMapper;
using Witcherius.BL.DataTransferObjects.Character;
using Witcherius.BL.Services.Common;
using Witcherius.Infrastructure;

namespace Witcherius.BL.Services.Character
{
    public class CharacterService : CrudService<Entities.Character, CharacterDto, CharacterCreateDto, CharacterUpdateDto>, ICharacterService
    {

        public CharacterService(IMapper mapper, IRepository<Entities.Character> repository) : base(mapper, repository)
        {
        }

        protected override async Task<Entities.Character> GetWithIncludesAsync(Guid entityId)
        {
            return await Repository.GetAsync(entityId, nameof(Entities.Character.Attributes),
                nameof(Entities.Character.Equipment), nameof(Entities.Character.Inventory));
        }

    }
}
