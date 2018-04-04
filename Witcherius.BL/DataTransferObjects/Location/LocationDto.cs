using System.Collections.Generic;
using Witcherius.BL.DataTransferObjects.Common;
using Witcherius.BL.DataTransferObjects.MiniLocation;

namespace Witcherius.BL.DataTransferObjects.Location
{
    public class LocationDto  : BaseDto
    {
        public string Name { get; set; }
        public ICollection<MiniLocationDto> MiniLocation { get; set; }
        public string Description { get; set; }
        public int RequiredLevel { get; set; }
        public string Image { get; set; }

        public LocationDto()
        {
            MiniLocation = new List<MiniLocationDto>();
        }
        public override string ToString()
        {
            return "location_" + Name;
        }
    }
}
