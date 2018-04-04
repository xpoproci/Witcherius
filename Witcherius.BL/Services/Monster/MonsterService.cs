using System.Threading.Tasks;
using AutoMapper;
using Witcherius.BL.DataTransferObjects.Monster;
using Witcherius.BL.Services.Common;
using Witcherius.Infrastructure;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Witcherius.BL.Services.Monster
{
    public class MonsterService : CrudService<Entities.Monster, MonsterDto, MonsterDto, MonsterEditDto>, IMonsterService
    {
        public MonsterService(IMapper mapper, IRepository<Entities.Monster> repository) : base(mapper, repository)
        {
        }

        protected override async Task<Entities.Monster> GetWithIncludesAsync(Guid entityId)
        {
            return await Repository.GetAsync(entityId, nameof(Entities.Monster.Name));
        }

        public async Task<MonsterDto> GetByName(string name)
        {
            Expression<Func<Entities.Monster, bool>> expr = a => a.Name.Equals(name);
            var location = (await ListAllAsync(expr)).FirstOrDefault();
            return Mapper.Map<MonsterDto>(location);
        }
    }
}
