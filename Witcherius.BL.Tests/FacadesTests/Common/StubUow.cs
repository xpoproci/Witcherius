using System.Threading.Tasks;
using Witcherius.Infrastructure.UnitOfWork;

namespace Witcherius.BL.Tests.FacadesTests.Common
{
    internal class StubUow : UnitOfWorkBase
    {
        protected override Task CommitCore()
        {
            return Task.Delay(15);
        }

        public override void Dispose()
        {
        }
    }
}
