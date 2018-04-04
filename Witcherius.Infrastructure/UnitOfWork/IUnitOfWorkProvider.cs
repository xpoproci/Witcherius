using System;

namespace Witcherius.Infrastructure.UnitOfWork
{
    public interface IUnitOfWorkProvider : IDisposable
    {
        IUnitOfWork Create();
        IUnitOfWork GetUnitOfWorkInstance();
    }
}
