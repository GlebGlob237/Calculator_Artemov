using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorLibrary_Artemov
{
    public class Calculator
    {
        public dynamic Slozenie(dynamic a, dynamic b)
        {
            return a + b;
        }
        public dynamic Vicitanie(dynamic a, dynamic b)
        {
            return a - b;
        }
        public dynamic Umnozenie(dynamic a, dynamic b)
        {
            return a * b;
        }
        public dynamic Delenie(dynamic a, dynamic b)
        {
            if (b == 0)
                throw new DivideByZeroException("Здесь нельзя делить на ноль :(Ата Та!)");
            else
                return a / b;
        }
        public dynamic Stepen(dynamic a, dynamic b)
        { 
            return Math.Pow(a,b); 
        }
    }
}
