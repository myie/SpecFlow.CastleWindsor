using Castle.MicroKernel.Lifestyle;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using CastleWindsor.SpecFlowPlugin;
using TechTalk.SpecFlow;

namespace Calculator.Tests.Support {
    public class SetupContainer {
        [ScenarioDependencies]
        public static IWindsorContainer CreateContainerBuilder()
        {
            // create container with the runtime dependencies
            //var container = Bootstrapper.GetContainer(); // to load from an existing container
            var container = new WindsorContainer(); // new container

            // begin scope to allow the Binding files and context to get recreated
            // before every scenario to ensure fresh dependencies.
            // If Scoped is not selected the dependencies will be created only at the beginning
            // of the test run
            container.BeginScope();

            container.Register(Types.FromAssemblyInThisApplication().Where(t => t.IsDefined(typeof(BindingAttribute), false)).LifestyleScoped());
            container.Install(FromAssembly.Containing<ICalculator>());
            container.Install(FromAssembly.This());


            return container;
        }
    }
}