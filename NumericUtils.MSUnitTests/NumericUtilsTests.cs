using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace NumericUtils.MSUnitTests
{
    [TestClass]
    public class NumericUtilsTests
    {
        [TestMethod]
        public void InsertNumber_From15To15CopyBitsFrom0To0_15Returned()
        {
            int destinationNumber = 15;
            int sourceNumber = 15;
            int highBitIndex = 0;
            int lowBitIndex = 0;
            int expectedNumber = 15;

            int actualNumber = NumericUtils.InsertNumber(destinationNumber, sourceNumber, highBitIndex, lowBitIndex);

            Assert.AreEqual(expectedNumber, actualNumber);
        }

        [TestMethod]
        public void InsertNumber_From15To8CopyBitsFrom0To0_9Returned()
        {
            int destinationNumber = 8;
            int sourceNumber = 15;
            int highBitIndex = 0;
            int lowBitIndex = 0;
            int expectedNumber = 9;

            int actualNumber = NumericUtils.InsertNumber(destinationNumber, sourceNumber, highBitIndex, lowBitIndex);

            Assert.AreEqual(expectedNumber, actualNumber);
        }

        [TestMethod]
        public void InsertNumber_From15To8CopyBitsFrom3To8_120Returned()
        {
            int destinationNumber = 8;
            int sourceNumber = 15;
            int highBitIndex = 8;
            int lowBitIndex = 3;
            int expectedNumber = 120;

            int actualNumber = NumericUtils.InsertNumber(destinationNumber, sourceNumber, highBitIndex, lowBitIndex);

            Assert.AreEqual(expectedNumber, actualNumber);
        }

        [TestMethod]
        public void InsertNumber_From1To1CopyBitsFromMinus3ToMinus2_ArgumentOutOfRangeExceptionThrown()
        {
            int destinationNumber = 1;
            int sourceNumber = 1;
            int highBitIndex = -2;
            int lowBitIndex = -3;

            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => NumericUtils.InsertNumber(destinationNumber, sourceNumber, highBitIndex, lowBitIndex));
        }

        [TestMethod]
        public void InsertNumber_From1To1CopyBitsFrom32To33_ArgumentOutOfRangeExceptionThrown()
        {
            int destinationNumber = 1;
            int sourceNumber = 1;
            int highBitIndex = 33;
            int lowBitIndex = 32;

            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => NumericUtils.InsertNumber(destinationNumber, sourceNumber, highBitIndex, lowBitIndex));
        }

        [TestMethod]
        public void InsertNumber_FromMinus1To1CopyBitsFrom6To4_ArgumentExceptionThrown()
        {
            int destinationNumber = 1;
            int sourceNumber = -1;
            int highBitIndex = 4;
            int lowBitIndex = 6;

            Assert.ThrowsException<ArgumentException>(
                () => NumericUtils.InsertNumber(destinationNumber, sourceNumber, highBitIndex, lowBitIndex));
        }
    }
}
