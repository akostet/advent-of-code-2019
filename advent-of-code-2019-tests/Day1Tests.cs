using advent_of_code_2019;
using NUnit.Framework;

namespace advent_of_code_2019_tests
{
    public class Day1Tests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void TestFuelCalculationExampleInput1()
        {
            var mass = 12;
            var expectedFuel = 2;
            var actualFuel = Day1.CalculateFuelForMass(mass);

            Assert.AreEqual(expectedFuel, actualFuel);
        }

        [Test]
        public void TestFuelCalculationExampleInput2()
        {
            var mass = 14;
            var expectedFuel = 2;
            var actualFuel = Day1.CalculateFuelForMass(mass);

            Assert.AreEqual(expectedFuel, actualFuel);
        }

        [Test]
        public void TestFuelCalculationExampleInput3()
        {
            var mass = 1969;
            var expectedFuel = 654;
            var actualFuel = Day1.CalculateFuelForMass(mass);

            Assert.AreEqual(expectedFuel, actualFuel);
        }

        [Test]
        public void TestFuelCalculationExampleInput4()
        {
            var mass = 100756;
            var expectedFuel = 33583;
            var actualFuel = Day1.CalculateFuelForMass(mass);

            Assert.AreEqual(expectedFuel, actualFuel);
        }

        [Test]
        public void TestFuelSumExampleInput1()
        {
            var mass = 14;
            var expectedFuel = 2;
            var actualFuel = Day1.SumOfFuels(mass, 0);

            Assert.AreEqual(expectedFuel, actualFuel);
        }

        [Test]
        public void TestFuelSumExampleInput2()
        {
            var mass = 1969;
            var expectedFuel = 966;
            var actualFuel = Day1.SumOfFuels(mass, 0);

            Assert.AreEqual(expectedFuel, actualFuel);
        }

        [Test]
        public void TestFuelSumExampleInput3()
        {
            var mass = 100756;
            var expectedFuel = 50346;
            var actualFuel = Day1.SumOfFuels(mass, 0);

            Assert.AreEqual(expectedFuel, actualFuel);
        }    
    }
}