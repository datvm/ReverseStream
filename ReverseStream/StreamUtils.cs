using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LukeVo.UtilityStream
{

    internal static class StreamUtils
    {

        public static void CheckSeekableUnderlyingStream(Stream underlyingStream)
        {
            if (underlyingStream == null)
            {
                throw new ArgumentNullException(nameof(underlyingStream));
            }

            if (!underlyingStream.CanRead)
            {
                throw new InvalidOperationException($"{nameof(underlyingStream)} {nameof(underlyingStream.CanRead)} is false");
            }

            if (!underlyingStream.CanSeek)
            {
                throw new InvalidOperationException($"{nameof(underlyingStream)} {nameof(underlyingStream.CanSeek)} is false");
            }
        }

    }

}
