using System.Collections.Generic;
using System.Threading.Tasks;
using Witcherius.BL.DataTransferObjects.Attributes;
using Witcherius.BL.DataTransferObjects.Character;
using Witcherius.BL.DataTransferObjects.Equipment;
using Witcherius.BL.DataTransferObjects.Inventory;
using Witcherius.BL.Facades.Common;
using Witcherius.BL.Services.Attributes;
using Witcherius.BL.Services.Character;
using Witcherius.BL.Services.Equipment;
using Witcherius.Infrastructure.UnitOfWork;
using System;
using Witcherius.BL.Enums;
using Witcherius.BL.Services.Inventory;

namespace Witcherius.BL.Facades
{
    public class CharacterFacade : BaseFacade
    {
        private readonly ICharacterService _characterService;
        private readonly IAttributesService _attributesService;
        private readonly IEquipmentService _equipmentService;

        public CharacterFacade(IUnitOfWorkProvider unitOfWorkProvider, ICharacterService characterService,
            IAttributesService attributesService, IEquipmentService equipmentService, IInventoryService inventoryService) : base(unitOfWorkProvider)
        {
            _characterService = characterService;
            _attributesService = attributesService;
            _equipmentService = equipmentService;
        }

        #region Crud
        public async Task<CharacterDto> GetAsync(Guid entityId)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await _characterService.GetAsync(entityId);
            }
        }


        public async Task<Guid> CreateAsync(CharacterCreateDto entityDto)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                //attributes, inventory, equipment
                var inv = new InventoryDto() { Id = Guid.NewGuid(), MaxSize = 12};
                var attr = GenerateAttributesAccordingToCharacter(entityDto);
                var eq = new EquipmentDto() { Id = Guid.NewGuid() };

                entityDto.CurrentHp = 10 * attr.Hp;
                entityDto.Inventory = inv;
                entityDto.Equipment = eq;
                entityDto.Attributes = attr;

                entityDto.Id = _characterService.Create(entityDto);
                await uow.Commit();
                return entityDto.Id;
            }
        }

        public async Task<bool> EditAsync(CharacterUpdateDto entityDto)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                if ((await _characterService.GetAsync(entityDto.Id, false)) == null)
                {
                    return false;
                }
                await _characterService.Update(entityDto);
                await uow.Commit();
                return true;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                if ((await _characterService.GetAsync(id, false)) == null)
                {
                    return false;
                }
                _characterService.Delete(id);
                await uow.Commit();
                return true;
            }
        }

        public async Task<IEnumerable<CharacterDto>> GetAllAsync()
        {
            using (UnitOfWorkProvider.Create())
            {
                return await _characterService.ListAllAsync();
            }
        }
        #endregion

        public AttributesDto GenerateAttributesAccordingToCharacter(CharacterCreateDto character)
        {
            var attr = new AttributesDto() { Id = Guid.NewGuid(), Damage = 10, Defense = 10, Hp = 15 };
            float[] raceMultiplier;
            float[] schoolMultiplier;
            switch (character.Race)
            {
                case Race.Human:
                    raceMultiplier = new[] { 1.8F, 1.8F, 1.8F };
                    break;
                case Race.Elf:
                    raceMultiplier = new[] { 2.2F, 1.6F, 1.6F };
                    break;
                case Race.Dwarf:
                    raceMultiplier = new[] { 1.7F, 2.0F, 2F };
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            switch (character.School)
            {
                case School.Wolf:
                    schoolMultiplier = new[] { 1.8F, 1.8F, 1.8F };
                    break;
                case School.Bear:
                    schoolMultiplier = new[] { 1.5F, 2.0F, 2.0F };
                    break;
                case School.Cat:
                    schoolMultiplier = new[] { 2.0F, 1.7F, 1.7F };
                    break;
                case School.Viper:
                    schoolMultiplier = new[] { 1.9F, 1.9F, 1.7F };
                    break;
                case School.Griffin:
                    schoolMultiplier = new[] { 1.7F, 1.8F, 2.0F };
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            attr.Damage = (int)(attr.Damage * raceMultiplier[0] * schoolMultiplier[0]);
            attr.Defense = (int)(attr.Defense * raceMultiplier[1] * schoolMultiplier[1]);
            attr.Hp = (int)(attr.Hp * raceMultiplier[2] * schoolMultiplier[2]);

            return attr;
        }

    }
}
