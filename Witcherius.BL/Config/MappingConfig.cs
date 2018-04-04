using AutoMapper;
using Witcherius.BL.DataTransferObjects.Attributes;
using Witcherius.BL.DataTransferObjects.Character;
using Witcherius.BL.DataTransferObjects.Equipment;
using Witcherius.BL.DataTransferObjects.Inventory;
using Witcherius.BL.DataTransferObjects.InventoryItems;
using Witcherius.BL.DataTransferObjects.Item;
using Witcherius.BL.DataTransferObjects.Location;
using Witcherius.BL.DataTransferObjects.Messages;
using Witcherius.BL.DataTransferObjects.MiniLocation;
using Witcherius.BL.DataTransferObjects.Monster;
using Witcherius.BL.DataTransferObjects.Quest;
using Witcherius.BL.DataTransferObjects.User;
using Witcherius.Entities;

namespace Witcherius.BL.Config
{
    public class MappingConfig
    {
        public static void ConfigureMapping(IMapperConfigurationExpression config)
        {
            config.CreateMap<InventoryItems, InventoryItemsDto>().ReverseMap();

            config.CreateMap<Attributes, AttributesDto>().ReverseMap();

            config.CreateMap<Equipment, EquipmentDto>().ReverseMap();
            config.CreateMap<Equipment, EquipmentEditDto>().ReverseMap();
            config.CreateMap<EquipmentEditDto, EquipmentDto>().ReverseMap();

            config.CreateMap<Character, CharacterDto>().ReverseMap();
            config.CreateMap<Character, CharacterCreateDto>().ReverseMap();
            config.CreateMap<Character, CharacterUpdateDto>().ReverseMap();
            config.CreateMap<CharacterDto, CharacterUpdateDto>().ReverseMap();
            config.CreateMap<CharacterDto, CharacterCreateDto>().ReverseMap();

            config.CreateMap<Location, LocationDto>().ReverseMap();

            config.CreateMap<MiniLocation, MiniLocationDto>().ReverseMap();
            config.CreateMap<MiniLocationEditDto, MiniLocationDto>().ReverseMap();
            config.CreateMap<MiniLocationEditDto, MiniLocation>().ReverseMap();

            config.CreateMap<Monster, MonsterDto>().ReverseMap();
            config.CreateMap<MonsterEditDto, MonsterDto>().ReverseMap();
            config.CreateMap<Monster, MonsterEditDto>().ReverseMap();

            config.CreateMap<Quest, QuestDto>().ReverseMap();
            config.CreateMap<QuestEditDto, QuestDto>().ReverseMap();
            config.CreateMap<Quest, QuestEditDto>().ReverseMap();

            config.CreateMap<User, UserDto>().ReverseMap();
            config.CreateMap<UserEditDto, UserDto>().ReverseMap();
            config.CreateMap<User, UserEditDto>().ReverseMap();
            config.CreateMap<UserCreateDto, UserDto>().ReverseMap();
            config.CreateMap<User, UserCreateDto>().ReverseMap();

            config.CreateMap<Item, ItemDto>().ReverseMap();
            config.CreateMap<ItemEditDto, ItemDto>().ReverseMap();
            config.CreateMap<Item, ItemEditDto>().ReverseMap();

            config.CreateMap<Message, MessageDto>().ReverseMap();
            config.CreateMap<Message, MessageEditDto>().ReverseMap();
            config.CreateMap<MessageEditDto, MessageDto>().ReverseMap();

            config.CreateMap<Inventory, InventoryDto>().ReverseMap();
            config.CreateMap<Inventory, InventoryEditDto>().ReverseMap();
            config.CreateMap<InventoryEditDto, InventoryDto>().ReverseMap();
        }
    }
}
