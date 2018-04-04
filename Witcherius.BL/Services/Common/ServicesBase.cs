using AutoMapper;

namespace Witcherius.BL.Services.Common
{
    public abstract class ServicesBase
    {
        protected readonly IMapper Mapper;

        protected ServicesBase(IMapper mapper)
        {
            Mapper = mapper;
        }
    }
}
