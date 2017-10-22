using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace NumericUtils.NUnitTests
{
    [TestFixture]
    public class NumericUtilsTests
    {
        [TestCase(15, 15, 0, 0, 15)]
        [TestCase(8, 15, 0, 0, 9)]
        [TestCase(8, 15, 8, 3, 120)]
        public void InsertNumber_CorrectValuesPassed_WorksCorrectly(int destinationNumber, int sourceNumber,
                                                                    int highBitIndex, int lowBitIndex,
                                                                    int expectedResult)
        {
            int actualResult = NumericUtils.InsertNumber(destinationNumber, sourceNumber, highBitIndex, lowBitIndex);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestCase(1, 2, -1, 0)]
        [TestCase(123, 123, 5, -8)]
        [TestCase(1421, 1241, 53, 5)]
        [TestCase(12, 12, 2, 54)]
        public void InsertNumber_IncorrectBitIndexPassed_ArgumentOutOfRangeExceptionThrown(int destinationNumber, 
                                                                                           int sourceNumber,
                                                                                           int highBitIndex,
                                                                                           int lowBitIndex)
        {
            Assert.Throws<ArgumentOutOfRangeException>(
                () => NumericUtils.InsertNumber(destinationNumber, sourceNumber, highBitIndex, lowBitIndex));
        }

        [TestCase(1, 1, 3, 4)]
        [TestCase(-142, -52, 30, 31)]
        public void InsertNumber_HighBitIndexIsLessThanLowBitIndex_ArgumentExceptionThrown(int destinationNumber,
                                                                                           int sourceNumber,
                                                                                           int highBitIndex,
                                                                                           int lowBitIndex)
        {
            Assert.Throws<ArgumentException>(
                () => NumericUtils.InsertNumber(destinationNumber, sourceNumber, highBitIndex, lowBitIndex));
        }

        [TestCase(12, 21)]
        [TestCase(513, 531)]
        [TestCase(2017, 2071)]
        [TestCase(414, 441)]
        [TestCase(144, 414)]
        [TestCase(1234321, 1241233)]
        [TestCase(1234126, 1234162)]
        [TestCase(3456432, 3462345)]
        [TestCase(10, -1)]
        [TestCase(20, -1)]
        public void FindNextBiggerNumber_CorrectNumberPassed_WorksCorrectly(int initialNumber, int expected)
        {
            int actual = NumericUtils.FindNextBiggerNumber(initialNumber);

            Assert.AreEqual(expected, actual);
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(Int32.MinValue)]
        public void FindNextBiggerNumber_NonPositiveNumberPassed_ArgumentOutOfRangeExceptionThrown(int initialNumber)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => NumericUtils.FindNextBiggerNumber(initialNumber));
        }

        [TestCase(Int32.MaxValue)]
        [TestCase(1333333332)]
        public void FindNextBiggerNumber_ReturnNumberIsBiggerThanHighestInteger_OverflowExceptionThrown(int initialNumber)
        {
            Assert.Throws<OverflowException>(() => NumericUtils.FindNextBiggerNumber(initialNumber));
        }

        [TestCase(1)]
        [TestCase(123456789)]
        [TestCase(198765432)]
        public void FindNextBiggerNumber_CorrectNumberPassed_ExecutionTimeIsApproximatelyCorrect(int initialNumber)
        {
            Stopwatch sw = new Stopwatch();

            sw.Start();
            NumericUtils.FindNextBiggerNumber(initialNumber, out long actualMilliseconds);
            sw.Stop();

            Assert.That(sw.ElapsedMilliseconds, Is.InRange(actualMilliseconds, actualMilliseconds + 1));
        }

        [TestCase(new int[] { 213142, 234514, 234153, 892384 }, 0, new int[] { })]
        [TestCase(new int[] { 3, 33, 333, 3333, 33333 }, 3, new int[] { 3, 33, 333, 3333, 33333 })]
        [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 7, 68, 69, 70, 15, 17 }, 7, new int[] { 7, 70, 17 })]
        [TestCase(new int[] { }, 5, new int[] { })]
        public void FilterDigit_CorrectValuesPassed_WorksCorrectly(IList<int> initialList, int digit, IList<int> expectedList)
        {
            IList<int> actualList = NumericUtils.FilterDigit(initialList, digit);

            CollectionAssert.AreEqual(expectedList, actualList);
        }

        [TestCase(null, -1)]
        [TestCase(null, 9)]
        public void FilterDigit_NullArgumentPassed_ArgumentNullExceptionThrown(IList<int> initialList, int digit)
        {
            Assert.Throws<ArgumentNullException>(() => NumericUtils.FilterDigit(initialList, digit));
        }

        [TestCase(new int[] { }, -2)]
        [TestCase(new int[] { }, 10)]
        public void FilterDigit_IncorrectDigitPassed_ArgumentOutOfRangeExceptionThrown(IList<int> initialList, int digit)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => NumericUtils.FilterDigit(initialList, digit));
        }

        [TestCase(1, 5, 0.0001, 1)]
        [TestCase(8, 3, 0.0001, 2)]
        [TestCase(0.001, 3, 0.0001, 0.1)]
        [TestCase(0.04100625, 4, 0.0001, 0.45)]
        [TestCase(8, 3, 0.0001, 2)]
        [TestCase(0.0279936, 7, 0.0001, 0.6)]
        [TestCase(0.0081, 4, 0.1, 0.3)]
        [TestCase(-0.008, 3, 0.1, -0.2)]
        [TestCase(0.004241979, 9, 0.00000001, 0.545)]
        public void FindNthRoot_CorrectValuesPassed_WorksCorrectly(double initialNumber, int degree, double precision, 
                                                                   double expectedValue)
        {
            double actualValue = NumericUtils.FindNthRoot(initialNumber, degree, precision);

            Assert.That(expectedValue, Is.EqualTo(NumericUtils.FindNthRoot(initialNumber, degree, precision)).Within(precision));
        }

        [TestCase(8, -23, 0.00000001)]
        [TestCase(8, 0, 0.001)]
        public void FindNthRoot_NonPositiveDegreePassed_ArgumentOutOfRangeExceptionThrown(double initialNumber, 
                                                                                          int degree, double precision)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => NumericUtils.FindNthRoot(initialNumber, degree, precision));
        }

        [TestCase(8, 15, -7)]
        [TestCase(8, 15, -0.6)]
        public void FindNthRoot_IncorrectPrecisionPassed_ArgumentOutOfRangeExceptionThrown(double initialNumber,
                                                                                           int degree, double precision)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => NumericUtils.FindNthRoot(initialNumber, degree, precision));
        }
    }
}
