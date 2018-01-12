# SpecFlow.CastleWindsor

This plugin replaces the default DI container in Specflow with Castle Windsor.

See the 'Examples' folder for actual examples of useage.

## Useage

### [ScenarioDependencies]

A Support folder should be added (automatically added by the nuget package) and contains a static method decorated with the [ScenarioDependencies] attribute.
Within this method we wire up our container & the specflow dependencies.

```csharp
var container = new WindsorContainer();
container.BeginScope();
container.Register(
    Types.FromAssemblyInThisApplication().Where(t => t.IsDefined(typeof(BindingAttribute), false)).LifestyleScoped());

return container;
```
This line is key and registers the Step files with the containers. You will see there is also registered a Context class that is used in the Steps
file to store values across the test steps. This shows how to wire up a custom context to the DI container.

The method with the ScenarioDependencies attribute is fired at the beginning of every scenario run
which allows us to use the Scoped lifecycle
The Windsor Container used here is a new WindsorContainer, but you could easily pull in an existing container if you have
a container with cross cutting concerns already registered for example.

### Constructor Injection

The steps file in the example shows how to use the dependencies, it should look familiar to anyone used
to Dependency Injection, simply add your dependencies to the Constructor.

```csharp
[binding]
 public class Steps {
        private readonly ICalculator _calculator;
        private readonly CalculatorContext _context;

        public Steps(ICalculator calculator,
                     CalculatorContext context) {
            _calculator = calculator;
            _context = context;
            _context.Numbers = new List<float>();
        }

		[When(@"I press add")]
        public void WhenIPressAdd() {
            _context.Result = _calculator.Add(_context.Numbers.ToArray());
        }
```

As you can see, you can then reference the service without any other work.

### App.Config

The app config requires a new piece of config added

```xml
  <plugins>
      <add name="CastleWindsor" type="Runtime"/>
    </plugins>
```

This wires up Specflow to the Windsor Plugin and is requried.


## Nuget

It is available as a Nuget package & this will inject a Support folder with a 
dependency registration file to help get started.

It also injects the plugin configuration into your app.config.

## Inspiration

This is based off the two existing Container Plugins for Specflow:

	* https://github.com/gasparnagy/SpecFlow.Autofac
	* https://github.com/phatcher/SpecFlow.Unity

It follows the same patterns found in these libraries.