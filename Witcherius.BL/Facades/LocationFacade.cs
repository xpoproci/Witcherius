using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Witcherius.BL.DataTransferObjects.Location;
using Witcherius.BL.DataTransferObjects.MiniLocation;
using Witcherius.BL.DataTransferObjects.Monster;
using Witcherius.BL.DataTransferObjects.Quest;
using Witcherius.BL.Facades.Common;
using Witcherius.BL.Services.Location;
using Witcherius.Infrastructure.UnitOfWork;

namespace Witcherius.BL.Facades
{
    public class LocationFacade : BaseFacade
    {
        private readonly ILocationService _locationService;

        public LocationFacade(IUnitOfWorkProvider unitOfWorkProvider, ILocationService locationService) : base(unitOfWorkProvider)
        {
            _locationService = locationService;
        }

        public async Task<LocationDto> GetLocationAsync(Guid id)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await _locationService.GetAsync(id);
            }
        }

        public async Task<Guid> CreateLocation(LocationDto location)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                location.Id = _locationService.Create(location);
                await uow.Commit();
                return location.Id;
            }
        }

        public async Task<bool> EditLocationAsync(LocationDto location)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                if ((await _locationService.GetAsync(location.Id, false)) == null)
                {
                    return false;
                }
                await _locationService.Update(location);
                await uow.Commit();
                return true;
            }
        }

        public async Task<bool> DeleteLocationAsync(Guid id)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                if ((await _locationService.GetAsync(id, false)) == null)
                {
                    return false;
                }
                _locationService.Delete(id);
                await uow.Commit();
                return true;
            }
        }

        public async Task<IEnumerable<LocationDto>> GetAllAsync()
        {
            using (UnitOfWorkProvider.Create())
            {
                return await _locationService.ListAllAsync();
            }
        }

        public async Task<LocationDto> GetByName(string name)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await _locationService.GetByName(name);
            }
        }
        public async Task<bool> AssignAsync(LocationDto location, MiniLocationDto miniLocation)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                if ((await _locationService.GetAsync(location.Id, false)) == null)
                {
                    return false;
                }
                await _locationService.Assign(location, miniLocation);
                await uow.Commit();
                return true;
            }
        }
    }
}
