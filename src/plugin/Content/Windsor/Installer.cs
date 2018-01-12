using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace CastleWindsor.SpecFlowPlugin.Content.Windsor {
    public class Installer : IWindsorInstaller {
        public void Install(IWindsorContainer container, IConfigurationStore store) {

            // TODO : add your context & test dependencies here
            // Scoped lifecycle should be used for contexts that need to be
            // re-created after every scenario. This ensures no cross
            // pollination of data which could affect downstream tests
           // container.Register(Component.For<IMyScenarioContext>().ImplementedBy<ScenarioContext>().LifestyleScoped());
        }
    }
}