using System;
using System.Threading.Tasks;

namespace Witcherius.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        Task Commit();
        void RegisterAction(Action action);
    }
}
