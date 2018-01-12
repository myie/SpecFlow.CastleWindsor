using CastleWindsor.SpecFlowPlugin;
using TechTalk.SpecFlow.Infrastructure;
using TechTalk.SpecFlow.Plugins;

[assembly: RuntimePlugin(typeof(CastleWindsorPlugin))]

namespace CastleWindsor.SpecFlowPlugin {
    public class CastleWindsorPlugin : IRuntimePlugin {
        public void Initialize(RuntimePluginEvents runtimePluginEvents, RuntimePluginParameters runtimePluginParameters) {

            // Wire up our Windsor resolver & container locater to the
            // global IOC container
            runtimePluginEvents.CustomizeGlobalDependencies += (sender, args) =>
                                                               {
                                                                   // temporary fix for CustomizeGlobalDependencies called multiple times
                                                                   // see https://github.com/techtalk/SpecFlow/issues/948
                                                                   if (args.ObjectContainer.IsRegistered<IContainerFinder>()) return;
                                                                   args.ObjectContainer.RegisterTypeAs<WindsorResolver, ITestObjectResolver>();
                                                                   args.ObjectContainer.RegisterTypeAs<ContainerFinder, IContainerFinder>();

                                                               };

            // Replace the IOC container with our windsor container
            // which is defined in a method marked with the [ScenarioDependecies] Attribute
            runtimePluginEvents.CustomizeScenarioDependencies += (sender, args) =>
                                                                 {
                                                                     args.ObjectContainer.RegisterFactoryAs(() =>
                                                                                                                             {
                                                                                                                                 var containerBuilderFinder = args.ObjectContainer.Resolve<IContainerFinder>();
                                                                                                                                 var containerBuilder = containerBuilderFinder.GetCreateScenarioContainer();
                                                                                                                                 var container = containerBuilder.Invoke();
                                                                                                                                 return container;
                                                                                                                             });
                                                                 };
        }
    }
}