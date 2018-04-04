using AutoMapper;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Witcherius.BL.Facades.Common;
using Witcherius.BL.Services.Common;
using Witcherius.Config;

namespace Witcherius.BL.Config
{
    public class BlInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            new DalInstallerI().Install(container, store);

            container.Register(

                Classes.FromThisAssembly()
                    .BasedOn<ServicesBase>()
                    .WithServiceDefaultInterfaces()
                    .LifestyleTransient(),

                Classes.FromThisAssembly()
                    .BasedOn<BaseFacade>()
                    .WithServiceDefaultInterfaces()
                    .LifestyleTransient(),

                Component.For<IMapper>()
                    .Instance(new Mapper(new MapperConfiguration(MappingConfig.ConfigureMapping)))
                    .LifestyleSingleton()
            );
        }
    }
}
