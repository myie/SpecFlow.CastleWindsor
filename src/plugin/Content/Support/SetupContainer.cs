using System;
using System.Linq;
using Castle.MicroKernel.Lifestyle;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using CastleWindsor.SpecFlowPlugin;
using TechTalk.SpecFlow;

namespace Integration.Tests.Support {
    public class SetupContainer {
        [ScenarioDependencies]
        public static IWindsorContainer CreateContainerBuilder()
        {
            // create container with the runtime dependencies
            //var container = Bootstrapper.GetContainer();
            var container = new WindsorContainer();

            // Begin scope, as this method is called before every scenario
            // anything using LifestyleScoped will also be recreated before
            // each scenario.
            container.BeginScope();
            container.Register(
                               // scoped to ensure fresh dependencies on every scenario - can be changed
                               // if you want your dependencies to exist for the entire test run
                               // usually want scoped.
                               Types.FromAssemblyInThisApplication().Where(t => t.IsDefined(typeof(BindingAttribute), false)).LifestyleScoped());
            

            return container;
        }
    }
}