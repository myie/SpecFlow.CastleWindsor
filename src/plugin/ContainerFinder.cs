using System;
using System.Linq;
using System.Reflection;
using Castle.Windsor;
using TechTalk.SpecFlow.Bindings;

namespace CastleWindsor.SpecFlowPlugin {
    public class ContainerFinder : IContainerFinder {

        private readonly IBindingRegistry _bindingRegistry;
        private readonly Lazy<Func<IWindsorContainer>> createScenarioContainer;

        /// <summary>
        /// Creates a new instance of the <see cref="ContainerFinder"/> class.
        /// </summary>
        /// <param name="bindingRegistry"></param>
        public ContainerFinder(IBindingRegistry bindingRegistry) {
            _bindingRegistry = bindingRegistry;
            createScenarioContainer = new Lazy<Func<IWindsorContainer>>(FindCreateScenarioContainer, true);
        }

        /// <copydoc cref="IContainerFinder.GetCreateScenarioContainer" />
        public Func<IWindsorContainer> GetCreateScenarioContainer() {
            var builder = createScenarioContainer.Value;
            if (builder == null)
            {
                throw new Exception("Unable to find scenario dependencies! Mark a static method that returns an IWindsorContainer with [ScenarioDependencies]!");
            }
            return builder;
        }

        public Func<IWindsorContainer> FindCreateScenarioContainer()
        {
            var assemblies = _bindingRegistry.GetBindingAssemblies();
            foreach (var assembly in assemblies)
            {
                foreach (var type in assembly.GetTypes())
                {
                    foreach (var methodInfo in type.GetMethods(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public).Where(m => Attribute.IsDefined((MemberInfo)m, typeof(ScenarioDependenciesAttribute))))
                    {
                        return () => (IWindsorContainer)methodInfo.Invoke(null, null);
                    }
                }
            }
            return null;
        }
    }
}