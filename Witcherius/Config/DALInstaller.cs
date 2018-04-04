using System;
using System.Data.Entity;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Castle.MicroKernel.Registration;
using Witcherius.Infrastructure;
using Witcherius.Infrastructure.UnitOfWork;

namespace Witcherius.Config
{
    public class DalInstallerI : IWindsorInstaller
    {

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<Func<DbContext>>()
                    .Instance(() => new WitcheriusDbContext())
                    .LifestyleTransient(),
                Component.For<IUnitOfWorkProvider>()
                    .ImplementedBy<EntityFrameworkUnitOfWorkProvider>()
                    .LifestyleSingleton(),
                Component.For(typeof(IRepository<>))
                    .ImplementedBy(typeof(MainRepository<>))
                    .LifestyleTransient()
            );
        }
    }
}
