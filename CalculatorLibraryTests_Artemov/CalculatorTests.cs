using Microsoft.VisualStudio.TestTools.UnitTesting;
using CalculatorLibraryTests_Artemov;
using System;
using CalculatorLibrary_Artemov;

namespace CalculatorLibraryTests_Artemov
{
    [TestClass]
    public class CalculatorTests
    {
        private Calculator Calc;
        [TestInitialize]
        public void Setup()
        {
            Calc = new Calculator();
        }
        [TestMethod]
        public void Slozenie_Polozitelnih()
        {
            double a = 5, b = 3,dolzno = 8;

            double NaDele = Calc.Slozenie(a, b);

            Assert.AreEqual(dolzno, NaDele, 0.001, "Должно быть 5 + 3 = 8");
        }
        [TestMethod]
        public void Slozenie_Otricatelnih()
        {
            double a = -5, b = -3, dolzno = -8;

            double NaDele = Calc.Slozenie(a, b);

            Assert.AreEqual(dolzno, NaDele, 0.001);
        }
        [TestMethod]
        public void Vicitanie_Polozitelnih()
        {
            double a = 10, b = 5, dolzno = 5;

            double NaDele = Calc.Vicitanie(a, b);

            Assert.AreEqual(dolzno, NaDele, 0.001);
        }
        [TestMethod]
        public void Vicitanie_Otricatelnih()
        {
            double a = -10, b = -5, dolzno = -5;

            double NaDele = Calc.Vicitanie(a, b);

            Assert.AreEqual(dolzno, NaDele, 0.001);
        }
        [TestMethod]
        public void Umnozenie_Polozitelnih()
        {
            double a = 2, b = 5, dolzno = 10;

            double NaDele = Calc.Umnozenie(a, b);

            Assert.AreEqual(dolzno, NaDele, 0.001);
        }
        [TestMethod]
        public void Umnozenie_Otricatelnih()
        {
            double a = -2, b = 5, dolzno = -10;

            double NaDele = Calc.Umnozenie(a, b);

            Assert.AreEqual(dolzno, NaDele, 0.001);
        }
        [TestMethod]
        public void Delenie_Polozitelnih()
        {
            double a = 10, b = 5, dolzno = 2;

            double NaDele = Calc.Delenie(a, b);

            Assert.AreEqual(dolzno, NaDele, 0.001);
        }
        [TestMethod]
        public void Delenie_Otricatelnih()
        {
            double a = -10, b = 5, dolzno = -2;

            double NaDele = Calc.Delenie(a, b);

            Assert.AreEqual(dolzno, NaDele, 0.001);
        }
        [TestMethod]
        public void Delenie_Na_Null()
        {
            double a = 10, b = 0;

            Assert.ThrowsException<DivideByZeroException>(() => Calc.Delenie(a, b));
        }
        [TestMethod]
        public void Stepen_Polozitelnih()
        {
            double a = 2, b = 3, dolzno = 8;

            double NaDele = Calc.Stepen(a, b);

            Assert.AreEqual(dolzno, NaDele, 0.001);
        }
        [TestMethod]
        public void Stepen_Otricatelnih()
        {
            double a = 2, b = -2, dolzno = 0.25;

            double NaDele = Calc.Stepen(a, b);

            Assert.AreEqual(dolzno, NaDele, 0.001);
        }
        [TestMethod]
        public void Stepen_B_Null ()
        {
            double a = 2, b = 0, dolzno = 1;

            double NaDele = Calc.Stepen(a, b);

            Assert.AreEqual(dolzno, NaDele, 0.001);
        }
        [TestMethod]
        public void Stepen_A_Null()
        {
            double a = 0, b = 2, dolzno = 0;

            double NaDele = Calc.Stepen(a, b);

            Assert.AreEqual(dolzno, NaDele, 0.001);
        }
    }
}