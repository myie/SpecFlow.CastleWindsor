using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Calculator.Windsor {
    public class Installer : IWindsorInstaller {
        public void Install(IWindsorContainer container, IConfigurationStore store) {
            container.Register(
                               Component.For<ICalculator>().ImplementedBy<Calculator>().LifestyleSingleton());
        }
    }
}