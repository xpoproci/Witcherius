using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Witcherius.BL.DataTransferObjects.Location;
using Witcherius.BL.DataTransferObjects.MiniLocation;
using Witcherius.BL.Services.Common;
using Witcherius.Infrastructure;

namespace Witcherius.BL.Services.Location
{
    public class LocationService : CrudService<Entities.Location, LocationDto, LocationDto, LocationDto>, ILocationService
    {
        private readonly IRepository<Entities.MiniLocation> _miniLocationRepository;

        public LocationService(IMapper mapper, IRepository<Entities.Location> repository,
            IRepository<Entities.MiniLocation> miniLocationRepository) : base(mapper, repository)
        {
            _miniLocationRepository = miniLocationRepository;
        }

        protected override async Task<Entities.Location> GetWithIncludesAsync(Guid entityId)
        {
            return await Repository.GetAsync(entityId);
        }

        public async Task<LocationDto> GetByName(string name)
        {
            Expression<Func<Entities.Location, bool>> expr = a => a.Name.Equals(name);
            var location = (await ListAllAsync(expr)).FirstOrDefault();
            return Mapper.Map<LocationDto>(location);
        }

        public async Task Assign(LocationDto location, MiniLocationDto miniLocation)
        {
            var l = await Repository.GetAsync(location.Id);
            var mL = await _miniLocationRepository.GetAsync(miniLocation.Id);
            l.MiniLocation.Add(mL);
            Repository.Update(l);
        }
    }
}
