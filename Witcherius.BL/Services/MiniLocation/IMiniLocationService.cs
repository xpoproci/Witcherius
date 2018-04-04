using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Witcherius.BL.DataTransferObjects.MiniLocation;
using Witcherius.BL.DataTransferObjects.Quest;

namespace Witcherius.BL.Services.MiniLocation
{
    public interface IMiniLocationService
    {
        Task<MiniLocationDto> GetAsync(Guid entityId, bool withIncludes = true);

        Guid Create(MiniLocationDto entityDto);

        Task Update(MiniLocationEditDto entityDto);

        void Delete(Guid entityId);

        Task<IEnumerable<MiniLocationDto>> ListAllForLocationAsync(string location);

        Task<MiniLocationDto> GetByName(string name);

        Task Assign(MiniLocationDto miniLocation, QuestDto quest);

        Task<IEnumerable<MiniLocationDto>> ListAllAsync();
    }
}
