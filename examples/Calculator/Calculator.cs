using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class Calculator : ICalculator {
        public float Add(params float[] values) {
            return values.Sum();
        }

        public float Subtract(params float[] values) {
            return values.Aggregate((c, n) => c - n);
        }
    }
}
