using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Witcherius.BL.DataTransferObjects.Character;
using Witcherius.BL.DataTransferObjects.Monster;
using Witcherius.BL.DataTransferObjects.Quest;

namespace Witcherius.BL.DataTransferObjects.MiniLocation
{
    public class InspectMiniLocationDto
    {
        public MiniLocationDto MiniLocationDto { get; set; }
        public MonsterDto MonsterDto { get; set; }
        public QuestDto QuestDto { get; set; }
        public CharacterDto CharacterDto { get; set; }
    }
}
