using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Witcherius.BL.DataTransferObjects.Equipment;
using Witcherius.BL.DataTransferObjects.Item;

namespace Witcherius.BL.Services.Equipment
{
    public interface IEquipmentService
    {
        Task<EquipmentDto> GetAsync(Guid entityId, bool withIncludes = true);

        Guid Create(EquipmentDto entityDto);

        Task EquipItemAsync(EquipmentDto eq, ItemDto item);

        Task Update(EquipmentEditDto entityDto);

        void Delete(Guid entityId);

        Task<IEnumerable<EquipmentDto>> ListAllAsync();
    }
}
