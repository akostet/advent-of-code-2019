using System;
using System.Collections.Generic;
using System.Text;
using advent_of_code_2019;
using NUnit.Framework;

namespace advent_of_code_2019_tests
{
    public class Day2Tests
    {
        [Test]
        public void TestOpProcessingWithExampleInput1()
        {
            var input = new List<long>() { 1, 0, 0, 0, 99 };
            var expectedOutput = new[] { 2, 0, 0, 0, 99 };
            var actualOutput = Day2.ProcessOpCodes(input);
            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [Test]
        public void TestOpProcessingWithExampleInput2()
        {
            var input = new List<long>() { 2, 3, 0, 3, 99 };
            var expectedOutput = new[] { 2, 3, 0, 6, 99 };
            var actualOutput = Day2.ProcessOpCodes(input);
            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [Test]
        public void TestOpProcessingWithExampleInput3()
        {
            var input = new List<long>() { 2, 4, 4, 5, 99, 0 };
            var expectedOutput = new[] { 2, 4, 4, 5, 99, 9801 };
            var actualOutput = Day2.ProcessOpCodes(input);
            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [Test]
        public void TestOpProcessingWithExampleInput4()
        {
            var input = new List<long>() { 1, 1, 1, 4, 99, 5, 6, 0, 99 };
            var expectedOutput = new[] { 30, 1, 1, 4, 2, 5, 6, 0, 99 };
            var actualOutput = Day2.ProcessOpCodes(input);
            Assert.AreEqual(expectedOutput, actualOutput);
        }
    }
}
