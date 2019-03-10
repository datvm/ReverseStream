using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReverseStream.Test
{

    internal static class TestUtils
    {

        public static byte[] TestData1 = new byte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 0, };
        public static byte[] TestData1Reverse = TestData1.Reverse().ToArray();
        public static byte[] TestData1ReverseLine = TestData1;

        public static byte[] TestData2 = new byte[] { 1, 2, 13, 4, 5, 6, 13, 8, 9, 0, };
        public static byte[] TestData2Reverse = TestData2.Reverse().ToArray();
        public static byte[] TestData2ReverseLine = new byte[] { 8, 9, 0, 13, 4, 5, 6, 13, 1, 2, };

    }

    public class TestDataClass : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { TestUtils.TestData1, TestUtils.TestData1Reverse, TestUtils.TestData1ReverseLine };
            yield return new object[] { TestUtils.TestData2, TestUtils.TestData2Reverse, TestUtils.TestData2ReverseLine };
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }

}
