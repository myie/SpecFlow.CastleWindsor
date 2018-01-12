using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Calculator.Tests.Windsor {
    public class Installer : IWindsorInstaller {
        public void Install(IWindsorContainer container, IConfigurationStore store) {

            // scoped lifecycle is selected to ensure a new context is generated after every scenario
            container.Register(
                               Component.For<CalculatorContext>().Instance(new CalculatorContext()).LifestyleScoped());
        }
    }
}