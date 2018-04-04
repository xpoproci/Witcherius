using System;
using System.Threading.Tasks;
using AutoMapper;
using Witcherius.BL.DataTransferObjects.Equipment;
using Witcherius.BL.DataTransferObjects.Item;
using Witcherius.BL.Enums;
using Witcherius.BL.Services.Common;
using Witcherius.Infrastructure;

namespace Witcherius.BL.Services.Equipment
{
    public class EquipmentService : CrudService<Entities.Equipment, EquipmentDto, EquipmentDto, EquipmentEditDto>, IEquipmentService
    {

        public EquipmentService(IMapper mapper, IRepository<Entities.Equipment> repository) : base(mapper, repository)
        {
        }

        protected override async Task<Entities.Equipment> GetWithIncludesAsync(Guid entityId)
        {
            return await Repository.GetAsync(entityId, nameof(Entities.Equipment));
        }

        public async Task EquipItemAsync(EquipmentDto eq, ItemDto item)
        {
            var equip = Mapper.Map<EquipmentEditDto>(eq);
            PlaceItemInRightPlace(equip, item);
            await Update(equip);
        }

        private static void PlaceItemInRightPlace(EquipmentEditDto eq, ItemDto i)
        {
            switch (i.ArmorClass)
            {
                case ArmorClass.Sword:
                    eq.SwordId = i.Id;
                    break;
                case ArmorClass.Armor:
                    eq.ArmorId = i.Id;
                    break;
                case ArmorClass.Trousers:
                    eq.TrousersId = i.Id;
                    break;
                case ArmorClass.Consumable:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

    }
}
