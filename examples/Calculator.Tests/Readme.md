# Calculator.Tests

This is our test project for the Calculator Service and performs two simple tests.

It has a direct assembly reference to the CastleWindsor.SpecFlowPlugin.

## Support

The Support folder contains a static method decorated with the [ScenarioDependencies] attribute.
Within this method we wire up our container & the specflow dependencies.

```
Types.FromAssemblyInThisApplication().Where(t => t.IsDefined(typeof(BindingAttribute), false))
```
This line is key and registers the Step files with the containers. You will see there is also registered a Context class that is used in the Steps
file to store values across the test steps. This shows how to wire up a custom context to the DI container.

The Windsor Container used here is a new WindsorContainer, but you could easily pull in an existing container if you have
a container with cross cutting concerns already registered for example.

## App.Config

The app config has a new piece of config added

```
  <plugins>
      <add name="CastleWindsor" type="Runtime"/>
    </plugins>
```

This wires up Specflow to the Windsor Plugin and is requried.

## Steps

The steps file shows how to use the dependencies, it should look familiar to anyone used
to Dependency Injection, simply add your dependencies to the Constructor.

```
 public class Steps {
        private readonly ICalculator _calculator;
        private readonly CalculatorContext _context;

        public Steps(ICalculator calculator,
                     CalculatorContext context) {
            _calculator = calculator;
            _context = context;
            _context.Numbers = new List<float>();
        }
```