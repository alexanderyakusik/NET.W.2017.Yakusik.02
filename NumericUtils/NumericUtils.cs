using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace NumericUtils
{
    public static class NumericUtils
    {
        /// <summary>
        /// Insert into <paramref name="destinationNumber"/> bits from <paramref name="highBitIndex"/> to
        /// <paramref name="lowBitIndex"/> from <paramref name="sourceNumber"/>
        /// </summary>
        /// <param name="destinationNumber">Number to which bits are copied</param>
        /// <param name="sourceNumber">Number from which bits are copied</param>
        /// <param name="highBitIndex">High index of bit to be copied</param>
        /// <param name="lowBitIndex">Low index of bit to be copied</param>
        /// <returns>Number with copied bits</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="highBitIndex"/> is not in range of 0..31</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="lowBitIndex"/> is not in range of 0..31</exception>
        /// <exception cref="ArgumentException"><paramref name="highBitIndex"/> is less than
        /// <paramref name="lowBitIndex"/></exception>
        public static int InsertNumber(int destinationNumber, int sourceNumber, int highBitIndex, int lowBitIndex)
        {
            const int INT_MAX_BIT_INDEX = 31;

            if (highBitIndex < 0 || highBitIndex > INT_MAX_BIT_INDEX || lowBitIndex < 0 || lowBitIndex > INT_MAX_BIT_INDEX)
            {
                throw new ArgumentOutOfRangeException();
            }

            if (highBitIndex < lowBitIndex)
            {
                throw new ArgumentException();
            }

            destinationNumber |= GetNumberBasedOnBitPosition(lowBitIndex, highBitIndex);
            int numberBasedOnCopiedBits = sourceNumber & GetNumberBasedOnBitPosition(0, highBitIndex - lowBitIndex);
            numberBasedOnCopiedBits <<= lowBitIndex;
            numberBasedOnCopiedBits |= GetNumberBasedOnBitPosition(0, lowBitIndex - 1);
            numberBasedOnCopiedBits |= GetNumberBasedOnBitPosition(highBitIndex + 1, INT_MAX_BIT_INDEX);
            destinationNumber &= numberBasedOnCopiedBits;

            return destinationNumber;
        }

        /// <summary>
        /// Finds the nearest number greater than the input <paramref name="number"/>,
        /// consisting of the same digits
        /// </summary>
        /// <param name="number">Input number</param>
        /// <returns>Nearest number greater than the input <paramref name="number"/>,
        /// consisting of the same digits or -1 if there isn't one</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="number"/> is non positive</exception>
        public static int FindNextBiggerNumber(int number)
        {
            ValidatePositiveNumber(number);

            int[] digits = SplitNumberIntoDigits(number);

            FindFirstDigitGreaterThanTheNext(digits, out int currentIndex);

            if (currentIndex + 1 == digits.Length)
            {
                return -1;
            }

            Swap(ref digits[currentIndex], ref digits[currentIndex + 1]);
            Array.Sort(digits, 0, currentIndex + 1, Comparer<int>.Create((x, y) => x < y ? 1 : (x == y ? 0 : -1)));
            Array.Reverse(digits);

            int result = GetNumberFromDigits(digits);
            if (result <= 0)
            {
                return -1;
            }

            return result;
        }

        /// <summary>
        /// Finds the nearest number greater than the input <paramref name="number"/>,
        /// consisting of the same digits, also returning method execution time in <paramref name="elapsedMilliseconds"/>
        /// </summary>
        /// <param name="number">Input number</param>
        /// <param name="elapsedMilliseconds">Method time execution</param>
        /// <returns>Nearest number greater than the input <paramref name="number"/>,
        /// consisting of the same digits or -1 if there isn't one</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="number"/> is non positive</exception>
        public static int FindNextBiggerNumber(int number, out long elapsedMilliseconds)
        {
            Stopwatch sw = new Stopwatch();

            sw.Start();
            int result = FindNextBiggerNumber(number);
            sw.Stop();

            elapsedMilliseconds = sw.ElapsedMilliseconds;

            return number;
        }

        /// <summary>
        /// Returns list containing values from <paramref name="list"/> that match
        /// the <paramref name="predicate"/>
        /// </summary>
        /// <param name="list">List to be filtered</param>
        /// <param name="predicate">Predicate to filter</param>
        /// <returns>Filtered list</returns>
        /// <exception cref="ArgumentNullException"><paramref name="list"/> is null</exception>
        /// <exception cref="ArgumentNullException"><paramref name="predicate"/> is null</exception>
        /// is not in the range of 0..9</exception>
        public static List<int> FilterDigit(IEnumerable<int> list, IPredicate<int> predicate)
        {
            if (list == null || predicate == null)
            {
                throw new ArgumentNullException();
            }

            var filteredList = new List<int>();

            foreach (int item in list)
            {
                if (predicate.IsTrue(item))
                {
                    filteredList.Add(item);
                }
            }

            return filteredList;
        }

        /// <summary>
        /// Finds a root of <paramref name="degree"/> of <paramref name="number"/> with the 
        /// specified <paramref name="precision"/>
        /// </summary>
        /// <param name="number">Initial number</param>
        /// <param name="degree">Degree of root</param>
        /// <param name="precision">Specified precision of the result</param>
        /// <returns>Root of <paramref name="degree"/> of <paramref name="number"/> with the 
        /// specified <paramref name="precision"/></returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="degree"/> is non positive</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="precision"/> is incorrect</exception>
        public static double FindNthRoot(double number, int degree, double precision)
        {
            ValidatePositiveNumber(degree);
            ValidatePrecision(precision);

            double next, current = 1;
            while (true)
            {
                next = (1.0 / degree) * ((degree - 1) * current + number / Math.Pow(current, degree - 1));
                if (Math.Abs(next - current) < precision)
                {
                    break;
                }
                current = next;
            }

            return next;
        }

        #region Private methods

        private static int GetNumberBasedOnBitPosition(int lowBitPosition, int highBitPosition)
        {
            int result = 0;

            for (int i = lowBitPosition; i <= highBitPosition; i++)
            {
                result |= 1 << i;
            }

            return result;
        }

        private static void ValidatePrecision(double precision)
        {
            if (precision < 0 || precision > 1)
            {
                throw new ArgumentOutOfRangeException();
            }
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

        private static int[] SplitNumberIntoDigits(int number)
        {
            int[] digits = new int[number.ToString().Length];
            int count = 0;

            while (number > 0)
            {
                digits[count++] = (number % 10);
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

        private static void Swap(ref int first, ref int second)
        {
            int temp = first;
            first = second;
            second = temp;
        }

        #endregion
    }
}
