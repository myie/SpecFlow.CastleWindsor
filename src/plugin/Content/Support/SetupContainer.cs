using Castle.MicroKernel.Lifestyle;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using TechTalk.SpecFlow;

namespace CastleWindsor.SpecFlowPlugin.Content.Support {
    /// <summary>
    /// SetupContainer is called before every scenario runs.
    /// It is used to setup the container &amp; its dependencies
    /// and then return the container to be used.
    ///  </summary>
    /// <remarks>
    ///  You can either add a 'new' windsor container as shown below
    /// or return an existing container as shown in the commented
    /// out example, this could be a static method that bootstraps
    /// your container & dependencies for you.
    /// </remarks>
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