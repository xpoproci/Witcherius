using System;
using System.Threading.Tasks;
using Witcherius.BL.DataTransferObjects.Attributes;
using Witcherius.BL.Facades.Common;
using Witcherius.BL.Services.Attributes;
using Witcherius.Infrastructure.UnitOfWork;

namespace Witcherius.BL.Facades
{
    public class AttributesFacade : BaseFacade
    {
        private readonly IAttributesService _attributesService;

        public AttributesFacade(IUnitOfWorkProvider unitOfWorkProvider, IAttributesService attributesService) : base(unitOfWorkProvider)
        {
            _attributesService = attributesService;
        }

        #region CRUD
        public async Task<AttributesDto> GetAttributesAsync(Guid id)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await _attributesService.GetAsync(id);
            }
        }

        public async Task<Guid> CreateAttributes(AttributesDto attributes)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                attributes.Id = _attributesService.Create(attributes);
                await uow.Commit();
                return attributes.Id;
            }
        }

        public async Task<bool> EditAttributesAsync(AttributesDto attr)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                if ((await _attributesService.GetAsync(attr.Id, false)) == null)
                {
                    return false;
                }
                await _attributesService.Update(attr);
                await uow.Commit();
                return true;
            }
        }

        public async Task<bool> DeleteAttributesAsync(Guid id)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                if ((await _attributesService.GetAsync(id, false)) == null)
                {
                    return false;
                }
                _attributesService.Delete(id);
                await uow.Commit();
                return true;
            }
        }


        #endregion


    }
}
