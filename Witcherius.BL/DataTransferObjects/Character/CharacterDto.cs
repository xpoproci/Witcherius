using System;
using System.Collections.Generic;
using Witcherius.BL.DataTransferObjects.Attributes;
using Witcherius.BL.DataTransferObjects.Common;
using Witcherius.BL.DataTransferObjects.Equipment;
using Witcherius.BL.DataTransferObjects.Inventory;
using Witcherius.BL.DataTransferObjects.Item;
using Witcherius.BL.Enums;

namespace Witcherius.BL.DataTransferObjects.Character
{
    public class CharacterDto : BaseDto
    {
        public int Gold { get; set; }
        public string Name { get; set; }
        public AttributesDto Attributes { get; set; }
        public int Experience { get; set; }
        public int CurrentHp { get; set; }
        public int SkillPoints { get; set; }
        public School School { get; set; }
        public InventoryDto Inventory { get; set; }
        public Race Race { get; set; }
        public EquipmentDto Equipment { get; set; }

        public DateTime? Sickness { get; set; }

        public Guid? InvetoryId { get; set; }
        public Guid? AttributesId { get; set; }
        public Guid? EquipmentId { get; set; }

        public int Level => (int)(Math.Floor(25 + Math.Sqrt(625 + 100 * Experience)) / 50);


        public string Avatar()
        {
            return $"{Race.ToString().ToLower()}_{School.ToString().ToLower()}.jpg";
        }

        public AttributesDto CalculateAttributes()
        {
            var result = new AttributesDto() {Damage = Attributes.Damage, Defense = Attributes.Defense, Hp = Attributes.Hp};
            var equip = new List<ItemDto>() {Equipment.Armor, Equipment.Sword, Equipment.Trousers};
            foreach (var itemDto in equip)
            {
                if(itemDto==null) continue;
                result.Damage += itemDto.Attributes.Damage;
                result.Hp += itemDto.Attributes.Hp;
                result.Defense += itemDto.Attributes.Defense;
            }
            return result;
        }

        public override bool Equals(object obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || !(GetType() == obj.GetType()))
            {
                return false;
            }
            var p = (CharacterDto)obj;
            return p.Id.Equals(Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
