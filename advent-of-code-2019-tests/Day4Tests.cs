using System;
using System.Collections.Generic;
using System.Text;
using advent_of_code_2019;
using NUnit.Framework;

namespace advent_of_code_2019_tests
{
    public class Day4Tests
    {
        [Test]
        public void TestIsSixDigitPassword()
        {
            var failingNumbers = new int[] {1, 13431, 9942, 999424242, 949949242, 22211124, 12};
            for (int i = 100000; i <= 999999; i++)
            {
                Assert.True(Day4.IsSixDigitNumber(i.ToString()));
            }

            foreach(var failingNumber in failingNumbers)
                Assert.False(Day4.IsSixDigitNumber(failingNumber.ToString()));
        }

        [Test]
        public void TestHasTwoAdjacentSameDigits()
        {
            var shouldFail = new int[] { 123456, 654321, 123789, 432643, 153141, 6363214 };
            var shouldPass = new int[] { 123356, 664321, 523321, 123455, 111111 };

            foreach (var failingNumber in shouldFail)
                Assert.False(Day4.HasTwoAdjacentSameDigits(failingNumber.ToString()));

            foreach (var passingNumber in shouldPass)
                Assert.True(Day4.HasTwoAdjacentSameDigits(passingNumber.ToString()));
        }
        [Test]
        public void TestPasswordNeverDecrease()
        {
            var shouldFail = new int[] { 123245, 666665, 654432, 234255, 167895, 334454 };
            var shouldPass = new int[] { 111111, 111112, 567777, 3344459, 146899 };

            foreach (var failingNumber in shouldFail)
                Assert.False(Day4.PasswordNeverDecrease(failingNumber.ToString()));

            foreach (var passingNumber in shouldPass)
                Assert.True(Day4.PasswordNeverDecrease(passingNumber.ToString()));
        }


        [Test]
        public void TestAllPasswordCriteria()
        {
            var shouldFail = new int[] { 223450, 123789 };
            var shouldPass = new int[] { 111111, 111123, 122345 };

            foreach (var failingNumber in shouldFail)
                Assert.False(Day4.MeetsCriteria(failingNumber));

            foreach (var passingNumber in shouldPass)
                Assert.True(Day4.MeetsCriteria(passingNumber));
        }

        [Test]
        public void TestHasTwoAdjacentSameDigitsExactlyTwice()
        {
            var shouldFail = new int[] { 123444 };
            var shouldPass = new int[] { 112233, 111122 };

            foreach (var failingNumber in shouldFail)
                Assert.False(Day4.HasTwoAdjacentSameDigitsExactlyTwice(failingNumber.ToString()));

            foreach (var passingNumber in shouldPass)
                Assert.True(Day4.HasTwoAdjacentSameDigitsExactlyTwice(passingNumber.ToString()));
        }

        
    }
}
