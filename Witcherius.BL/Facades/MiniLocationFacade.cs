using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Witcherius.BL.DataTransferObjects.MiniLocation;
using Witcherius.BL.DataTransferObjects.Quest;
using Witcherius.BL.Facades.Common;
using Witcherius.BL.Services.MiniLocation;
using Witcherius.Infrastructure.UnitOfWork;

namespace Witcherius.BL.Facades
{
    public class MiniLocationFacade : BaseFacade
    {
        private readonly IMiniLocationService _miniLocationService;
        public MiniLocationFacade(IUnitOfWorkProvider unitOfWorkProvider, IMiniLocationService miniLocationService) : base(unitOfWorkProvider)
        {
            _miniLocationService = miniLocationService;
        }

        public async Task<MiniLocationDto> GetMiniLocationAsync(Guid id)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await _miniLocationService.GetAsync(id);
            }
        }

        public async Task<Guid> CreateMiniLocation(MiniLocationDto miniLocation)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                miniLocation.Id = _miniLocationService.Create(miniLocation);
                await uow.Commit();
                return miniLocation.Id;
            }
        }

        public async Task<bool> EditMiniLocationAsync(MiniLocationEditDto location)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                if ((await _miniLocationService.GetAsync(location.Id, false)) == null)
                {
                    return false;
                }
                await _miniLocationService.Update(location);
                await uow.Commit();
                return true;
            }
        }

        public async Task<bool> DeleteMiniLocationAsync(Guid id)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                if ((await _miniLocationService.GetAsync(id, false)) == null)
                {
                    return false;
                }
                _miniLocationService.Delete(id);
                await uow.Commit();
                return true;
            }
        }

        public async Task<IEnumerable<MiniLocationDto>> GetAllForLocationAsync(string name)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await _miniLocationService.ListAllForLocationAsync(name);
            }
        }

        public async Task<MiniLocationDto> GetByName(string name)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await _miniLocationService.GetByName(name);
            }
        }

        public async Task<bool> AssignAsync(MiniLocationDto miniLocation, QuestDto quest)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                if ((await _miniLocationService.GetAsync(miniLocation.Id, false)) == null)
                {
                    return false;
                }
                await _miniLocationService.Assign(miniLocation, quest);
                await uow.Commit();
                return true;
            }
        }

        public async Task<IEnumerable<MiniLocationDto>> GetAllItemsAsync()
        {
            using (UnitOfWorkProvider.Create())
            {
                return await _miniLocationService.ListAllAsync();
            }
        }
    }
}
