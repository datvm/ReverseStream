using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO
{

    public class ReverseStream : Stream
    {

        public ReverseStream(Stream underlyingStream)
        {
            this.UnderlyingStream = underlyingStream;
        }

        public Stream UnderlyingStream { get; private set; }

        public override bool CanRead => true;

        public override bool CanSeek => this.UnderlyingStream.CanSeek;

        public override bool CanWrite => false;

        public override long Length => this.UnderlyingStream.Length;

        public override long Position { get; set; }

        public long UnderlyingPosition
        {
            get
            {
                return this.GetUnderlyingPosition(this.Position);
            }
        }

        public override void Flush()
        {
            throw new NotImplementedException();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            var to = Math.Min(this.UnderlyingStream.Length, this.GetUnderlyingPosition(this.Position));
            var from = Math.Max(0, this.GetUnderlyingPosition(this.Position + count - 1));

            if (to < from)
            {
                return 0;
            }

            count = (int)(to - from + 1);
            this.UnderlyingStream.Seek(from, SeekOrigin.Begin);

            var reverseBuffer = new byte[count];
            var byteReadCount = this.UnderlyingStream.Read(reverseBuffer, 0, count);

            for (int i = 0; i < byteReadCount; i++)
            {
                buffer[offset + i] = reverseBuffer[byteReadCount - i - 1];
            }

            this.Position += byteReadCount;
            return byteReadCount;
        }

        public long GetUnderlyingPosition(long position)
        {
            if (position < 0)
            {
                throw new IndexOutOfRangeException();
            }

            return this.UnderlyingStream.Length - position - 1;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            var from = 0L;

            switch (origin)
            {
                case SeekOrigin.Current:
                    from = this.Position;
                    break;
                case SeekOrigin.End:
                    from = this.Length;
                    break;
                case SeekOrigin.Begin:
                default:
                    from = 0;
                    break;
            }

            return this.Position = from + offset;
        }

        public override void SetLength(long value)
        {
            this.UnderlyingStream.SetLength(value);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {


            this.UnderlyingStream.Seek(this.UnderlyingPosition, SeekOrigin.Begin);

            var writing = new byte[count];
            var lastIndex = offset + count;
            for (int i = 0; i < count; i++)
            {
                writing[i] = buffer[lastIndex - i - 1];
            }
            this.UnderlyingStream.Write(writing, 0, count);
        }

    }

}
