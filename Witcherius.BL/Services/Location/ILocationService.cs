using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Witcherius.BL.DataTransferObjects.Location;
using Witcherius.BL.DataTransferObjects.MiniLocation;

namespace Witcherius.BL.Services.Location
{
    public interface ILocationService
    {
        Task<LocationDto> GetAsync(Guid entityId, bool withIncludes = true);

        Guid Create(LocationDto entityDto);

        Task Update(LocationDto entityDto);

        void Delete(Guid entityId);

        Task<IEnumerable<LocationDto>> ListAllAsync();

        Task<LocationDto> GetByName(string name);

        Task Assign(LocationDto location, MiniLocationDto miniLocation);
    }
}
