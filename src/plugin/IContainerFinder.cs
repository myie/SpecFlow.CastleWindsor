using System;
using Castle.Windsor;

namespace CastleWindsor.SpecFlowPlugin {
        /// <summary>
        /// Responsible for creating the container used by a scenario
        /// </summary>
        public interface IContainerFinder
        {
        /// <summary>
        /// Get or create the <see cref="IWindsorContainer"/> to be used by a scenario.
        /// </summary>
        /// <returns></returns>
        Func<IWindsorContainer> GetCreateScenarioContainer();
        }
}