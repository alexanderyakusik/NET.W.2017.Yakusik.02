using NUnit.Framework;
using System;
using System.Diagnostics;

namespace NumericUtils.NUnitTests
{
    [TestFixture]
    public class NumericUtilsTests
    {
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
            Assert.Catch<ArgumentOutOfRangeException>(() => NumericUtils.FindNextBiggerNumber(initialNumber));
        }

        [TestCase(Int32.MaxValue)]
        [TestCase(1333333332)]
        public void FindNextBiggerNumber_ReturnNumberIsBiggerThanHighestInteger_OverflowExceptionThrown(int initialNumber)
        {
            Assert.Catch<OverflowException>(() => NumericUtils.FindNextBiggerNumber(initialNumber));
        }

        [TestCase(1)]
        [TestCase(123456789)]
        [TestCase(198765432)]
        public void FindNextBiggerNumber_CorrectNumberPassed_ExecutionTimeIsCorrect(int initialNumber)
        {
            Stopwatch sw = new Stopwatch();

            sw.Start();
            NumericUtils.FindNextBiggerNumber(initialNumber, out long actualMilliseconds);
            sw.Stop();

            Assert.AreEqual(sw.ElapsedMilliseconds, actualMilliseconds);
        }
    }
}
