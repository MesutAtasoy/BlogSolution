using Autofac;
using BlogSolution.Mongo;
using Stats.Application.Repositories;
using Stats.Domain.Models;

namespace Stats.Application.Modules
{
    public class ApplicationModule : Module
    {
        public ApplicationModule()
        {

        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.AddMongoRepository<BlogStatsItem>("BlogStatsItems");
            
            builder.RegisterType<BlogStatsItemRepository>()
                .As<IBlogStatsItemRepository>()
                .InstancePerLifetimeScope();
            
        }
    }
}
