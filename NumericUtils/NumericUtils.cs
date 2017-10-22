using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace NumericUtils
{
    public static class NumericUtils
    {
        public static int InsertNumber(int destinationNumber, int sourceNumber, int highBitIndex, int lowBitIndex)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Finds the nearest number greater than the input <paramref name="number"/> parameter,
        /// consisting of the same digits.
        /// </summary>
        /// <param name="number">Input number</param>
        /// <returns>Nearest number greater than the input <paramref name="number"/> parameter,
        /// consisting of the same digits</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="number"/> is non positive.</exception>
        /// <exception cref="OverflowException">Return value is not in the range of System.Int32 type.</exception>
        public static int FindNextBiggerNumber(int number)
        {
            ValidatePositiveNumber(number);

            if (number < 10)
            {
                return -1;
            }

            List<int> digits = SplitNumberIntoDigits(number);

            FindFirstDigitGreaterThanTheNext(digits, out int currentIndex);

            if (currentIndex + 1 == digits.Count)
            {
                return -1;
            }

            digits.Swap(currentIndex, currentIndex + 1);
            digits.Sort(0, currentIndex + 1, Comparer<int>.Create((x, y) => x < y ? 1 : (x == y ? 0 : -1)));
            digits.Reverse();

            int result = GetNumberFromDigits(digits);
            if (result <= 0)
            {
                throw new OverflowException();
            }

            return result;
        }

        /// <summary>
        /// Finds the nearest number greater than the input <paramref name="number"/> parameter,
        /// consisting of the same digits.
        /// </summary>
        /// <param name="number">Input number</param>
        /// <param name="elapsedMilliseconds">Method time execution</param>
        /// <returns>Nearest number greater than the input <paramref name="number"/> parameter,
        /// consisting of the same digits</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="number"/> is non positive.</exception>
        /// <exception cref="OverflowException">Return value is not in the range of System.Int32 type.</exception>
        public static int FindNextBiggerNumber(int number, out long elapsedMilliseconds)
        {
            Stopwatch sw = new Stopwatch();

            sw.Start();
            int result = FindNextBiggerNumber(number);
            sw.Stop();

            elapsedMilliseconds = sw.ElapsedMilliseconds;

            return number;
        }

        private static void ValidatePositiveNumber(int number)
        {
            if (number <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        private static void FindFirstDigitGreaterThanTheNext(IList<int> list, out int digitIndex)
        {
            digitIndex = 0;
            while (digitIndex + 1 < list.Count && list[digitIndex] <= list[digitIndex + 1])
            {
                digitIndex++;
            }
        }

        private static List<int> SplitNumberIntoDigits(int number)
        {
            List<int> digits = new List<int>();

            while (number > 0)
            {
                digits.Add(number % 10);
                number /= 10;
            }

            return digits;
        }

        private static int GetNumberFromDigits(IList<int> digits)
        {
            int number = 0, multiplier = 1;

            for (int i = digits.Count - 1; i >= 0; i--)
            {
                number += multiplier * digits[i];
                multiplier *= 10;
            }

            return number;
        }

        private static void Swap<T>(this List<T> list, int firstIndex, int secondIndex)
        {
            T temp = list[firstIndex];
            list[firstIndex] = list[secondIndex];
            list[secondIndex] = temp;
        }

        public static void FilterDigit(IList<int> list, int digit)
        {
            throw new NotImplementedException();
        }

        public static double FindNthRoot(double number, int degree, double precision)
        {
            throw new NotImplementedException();
        }
    }
}
