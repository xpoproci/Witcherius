using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Witcherius.BL.DataTransferObjects.MiniLocation;
using Witcherius.BL.DataTransferObjects.Quest;
using Witcherius.BL.Services.Common;
using Witcherius.Infrastructure;

namespace Witcherius.BL.Services.MiniLocation
{
    public class MiniLocationService : CrudService<Entities.MiniLocation, MiniLocationDto, MiniLocationDto, MiniLocationEditDto>, IMiniLocationService
    {
        public MiniLocationService(IMapper mapper, IRepository<Entities.MiniLocation> repository) : base(mapper, repository)
        {
        }

        protected override async Task<Entities.MiniLocation> GetWithIncludesAsync(Guid entityId)
        {
            return await Repository.GetAsync(entityId);
        }

        public async Task<IEnumerable<MiniLocationDto>> ListAllForLocationAsync(string location)
        {
            Expression<Func<Entities.MiniLocation, bool>> expr = a => a.Name.Equals(location);
            var mLocation = (await ListAllAsync(expr)).AsEnumerable();
            return mLocation;
        }

        public async Task<MiniLocationDto> GetByName(string name)
        {
            Expression<Func<Entities.MiniLocation, bool>> expr = a => a.Name.Equals(name);
            var location = (await ListAllAsync(expr)).FirstOrDefault();
            return Mapper.Map<MiniLocationDto>(location);
        }

        public async Task Assign(MiniLocationDto miniLocation, QuestDto quest)
        {
            var edited = Mapper.Map<MiniLocationEditDto>(miniLocation);
            edited.QuestId = quest.Id;
            await Update(edited);
        }
    }
}
