using LukeVo.UtilityStream;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ReverseStream
{
    public class ReverseDelimeterStream : Stream
    {

        public ReverseDelimeterStream(Stream underlyingStream, char delimeter)
        {
            StreamUtils.CheckSeekableUnderlyingStream(underlyingStream);

            this.UnderlyingStream = underlyingStream;
            this.Delimeter = delimeter;
        }

        public Stream UnderlyingStream { get; private set; }

        public char Delimeter { get; private set; }

        public override bool CanRead => true;

        public override bool CanSeek => this.UnderlyingStream.CanSeek;

        public override bool CanWrite => false;

        public override long Length => this.UnderlyingStream.Length;

        public override long Position { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void Flush()
        {
            throw new NotImplementedException();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotImplementedException();
        }

        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }
    }
}
