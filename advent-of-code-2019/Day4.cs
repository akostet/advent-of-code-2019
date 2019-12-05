using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace advent_of_code_2019
{
    public static class Day4
    {
        public static int Problem1(int low, int high)
        {
            var count = 0;
            for (var currentPassword = low; currentPassword <= high; currentPassword++)
            {
                if (MeetsCriteria(currentPassword))
                    count++;
            }

            return count;
        }

        public static int Problem2(int low, int high)
        {
            var count = 0;
            for (var currentPassword = low; currentPassword <= high; currentPassword++)
            {
                if (MeetsCriteria(currentPassword) && HasTwoAdjacentSameDigitsExactlyTwice(currentPassword.ToString()))
                    count++;
            }

            return count;
        }

        public static bool MeetsCriteria(int password)
        {
            var stringPassword = password.ToString();

            return IsSixDigitNumber(stringPassword) && 
                   HasTwoAdjacentSameDigits(stringPassword) &&
                   PasswordNeverDecrease(stringPassword);
        }

        public static bool IsSixDigitNumber(string password)
        {
            return password.Length == 6;
        }

        public static bool HasTwoAdjacentSameDigits(string password)
        {
            var previousChar = 'a';
            foreach (var c in password)
            {
                if (c == previousChar)
                    return true;
                previousChar = c;
            }

            return false;
        }

        public static bool PasswordNeverDecrease(string password)
        {
            var previousChar = '0';
            foreach (var c in password)
            {
                if (c < previousChar)
                    return false;
                previousChar = c;
            }

            return true;
        }

        public static bool HasTwoAdjacentSameDigitsExactlyTwice(string password)
        {
            var previousChar = 'a';
            var matchCount = 0;

            foreach (var c in password)
            {
                if (c == previousChar)
                    matchCount++;
                else
                {
                    if (matchCount == 1)
                        return true;

                    matchCount = 0;
                }

                previousChar = c;
            }

            return matchCount == 1;
        }

    }
}
