using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xunit;

namespace ReverseStream.Test
{

    public class ReverseStreamTest
    {

        [Fact]
        public void ShouldCheckContracts()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new System.IO.ReverseStream(null);
            });

            Assert.Throws<InvalidOperationException>(() =>
            {
                new System.IO.ReverseStream(new TestStream()
                {
                    CanSeekField = false,
                });
            });

            Assert.Throws<InvalidOperationException>(() =>
            {
                new System.IO.ReverseStream(new TestStream()
                {
                    CanReadField = false,
                });
            });
        }

        [Theory]
        [ClassData(typeof(TestDataClass))]
        public void ShouldHaveReversedResult(byte[] input, byte[] expectedReverse, byte[] _)
        {
            var output = new byte[input.Length];
            int byteCount;

            using (var inputStream = new MemoryStream(input))
            {
                using (var reverseStream = new System.IO.ReverseStream(inputStream))
                {
                    byteCount = reverseStream.Read(output, 0, output.Length);

                    Assert.Equal(input.Length, reverseStream.Position);
                }
            }

            Assert.Equal(input.Length, byteCount);
            Assert.Equal(expectedReverse, output);
        }

        [Theory]
        [ClassData(typeof(TestDataClass))]
        public void ShouldHaveCorrectSplittedResult(byte[] input, byte[] expectedReverse, byte[] _)
        {
            var output = new byte[input.Length];
            int byteCount;
            var segmentLength = input.Length / 5;

            using (var inputStream = new MemoryStream(input))
            {
                using (var reverseStream = new System.IO.ReverseStream(inputStream))
                {
                    for (int i = 0; i < input.Length; i += segmentLength)
                    {
                        Assert.Equal(i, reverseStream.Position);
                        byteCount = reverseStream.Read(output, i, segmentLength);

                        Assert.Equal(
                            Math.Min(segmentLength, input.Length - i),
                            byteCount);

                        Assert.Equal(
                            expectedReverse.Take(i + segmentLength),
                            output.Take(i + segmentLength));
                    }

                    byteCount = reverseStream.Read(output, 0, int.MaxValue);
                    Assert.Equal(0, byteCount);
                }
            }

            Assert.Equal(expectedReverse, output);
        }

        [Theory]
        [ClassData(typeof(TestDataClass))]
        public void ShouldSeekCorrectly(byte[] input, byte[] expectedReverse, byte[] _)
        {
            var segmentLength = input.Length / 3;
            var start = segmentLength;

            var output = new byte[segmentLength];
            using (var inputStream = new MemoryStream(input))
            {
                using (var reverseStream = new System.IO.ReverseStream(inputStream))
                {
                    reverseStream.Seek(start, SeekOrigin.Begin);
                    var count = reverseStream.Read(output, 0, segmentLength);

                    Assert.Equal(segmentLength, count);
                    Assert.Equal(
                        expectedReverse.Skip(start).Take(segmentLength),
                        output);
                }
            }
        }

    }

}
