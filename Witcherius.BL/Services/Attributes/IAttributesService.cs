using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Witcherius.BL.DataTransferObjects.Attributes;

namespace Witcherius.BL.Services.Attributes
{
    public interface IAttributesService
    {
        Task<AttributesDto> GetAsync(Guid entityId, bool withIncludes = true);

        Guid Create(AttributesDto entityDto);

        Task Update(AttributesDto entityDto);

        void Delete(Guid entityId);

        Task<IEnumerable<AttributesDto>> ListAllAsync();
    }
}
