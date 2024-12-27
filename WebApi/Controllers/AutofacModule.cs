using Autofac;
using Application.Interfaces;
using Application.UseCases;
using Infrastructure.Data;

namespace WebApi.Configurations
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Register DbContext
            builder.RegisterType<BlockedDomainContext>().AsSelf().InstancePerLifetimeScope();

            // Register Unit of Work
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();

            // Register Services
            builder.RegisterType<BlockedDomainService>().As<IBlockedDomainService>().InstancePerLifetimeScope();

            // Register Trie and Domain Block Checker
            builder.RegisterType<Trie>().AsSelf().SingleInstance();
            builder.RegisterType<TrieDomainBlockChecker>().As<IDomainBlockChecker>().InstancePerLifetimeScope();
        }
    }
}
