using System.Threading.Tasks;
using AutoMapper;
using Witcherius.BL.DataTransferObjects.Attributes;
using Witcherius.BL.Services.Common;
using Witcherius.Infrastructure;
using System;

namespace Witcherius.BL.Services.Attributes
{
    public class AttributesService : CrudService<Entities.Attributes, AttributesDto, AttributesDto, AttributesDto>, IAttributesService
    {
        public AttributesService(IMapper mapper, IRepository<Entities.Attributes> repository) : base(mapper, repository)
        {
        }

        protected override async Task<Entities.Attributes> GetWithIncludesAsync(Guid entityId)
        {
            return await Repository.GetAsync(entityId);
        }
    }
}
