using Witcherius.Infrastructure.UnitOfWork;

namespace Witcherius.BL.Facades.Common
{
    public abstract class BaseFacade
    {
        protected readonly IUnitOfWorkProvider UnitOfWorkProvider;

        protected BaseFacade(IUnitOfWorkProvider unitOfWorkProvider)
        {
            UnitOfWorkProvider = unitOfWorkProvider;
        }
    }
}
