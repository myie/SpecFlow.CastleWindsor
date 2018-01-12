using System.Collections.Generic;
using FluentAssertions;
using TechTalk.SpecFlow;

namespace Calculator.Tests {
    [Binding]
    public class Steps {
        private readonly ICalculator _calculator;
        private readonly CalculatorContext _context;

        public Steps(ICalculator calculator,
                     CalculatorContext context) {
            _calculator = calculator;
            _context = context;
            _context.Numbers = new List<float>();
        }

        [Given(@"I have entered (.*) into the calculator")]
        public void GivenIHaveEnteredIntoTheCalculator(int p0)
        {
            _context.Numbers.Add(p0);
        }

        [When(@"I press add")]
        public void WhenIPressAdd() {
            _context.Result = _calculator.Add(_context.Numbers.ToArray());
        }

        [Then(@"the result should be (.*) on the screen")]
        public void ThenTheResultShouldBeOnTheScreen(int p0) {
            _context.Result.Should().Be(p0);
        }

        [When(@"I press subtract")]
        public void WhenIPressSubtract()
        {
            _context.Result = _calculator.Subtract(_context.Numbers.ToArray());
        }
    }

    public class CalculatorContext
    {
        public List<float> Numbers { get; set; }
        public float Result { get; set; }
    }
}