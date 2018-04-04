using System;
using System.ComponentModel.DataAnnotations;
using Witcherius.BL.DataTransferObjects.Attributes;
using Witcherius.BL.DataTransferObjects.Common;
using Witcherius.BL.DataTransferObjects.Equipment;
using Witcherius.BL.DataTransferObjects.Inventory;
using Witcherius.BL.Enums;

namespace Witcherius.BL.DataTransferObjects.Character
{
    public class CharacterCreateDto : BaseDto
    {
        [Required(ErrorMessage = "User name is required!")]
        [MaxLength(64, ErrorMessage = "Your User name is too long!")]
        public string Name { get; set; }

        public int CurrentHp { get; set; }

        public School School { get; set; }
        public Race Race { get; set; }
        public AttributesDto Attributes { get; set; }
        public InventoryDto Inventory { get; set; }
        public EquipmentDto Equipment { get; set; }

        public Guid UserId { get; set; }
    }
}
