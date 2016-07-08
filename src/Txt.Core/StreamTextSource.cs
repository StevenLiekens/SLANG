﻿using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using JetBrains.Annotations;

namespace Txt.Core
{
    public class StreamTextSource : TextSource
    {
        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly BinaryReader binaryReader;

        public StreamTextSource([NotNull] PushbackInputStream inputStream, [NotNull] Encoding encoding)
        {
            if (inputStream == null)
            {
                throw new ArgumentNullException(nameof(inputStream));
            }
            if (encoding == null)
            {
                throw new ArgumentNullException(nameof(encoding));
            }
            Encoding = encoding;
            binaryReader = new BinaryReader(inputStream, encoding, true);
        }

        public Encoding Encoding { get; }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                binaryReader.Dispose();
            }
            base.Dispose(disposing);
        }

        protected override int PeekImpl()
        {
            if (binaryReader.BaseStream.CanSeek)
            {
                return binaryReader.PeekChar();
            }

            var next = ReadImpl();
            if (next == -1)
            {
                return -1;
            }
            UnreadImpl((char)next);
            return next;
        }

        protected override int ReadImpl()
        {
            return binaryReader.Read();
        }

        protected override int ReadImpl(char[] buffer, int offset, int count)
        {
            return binaryReader.Read(buffer, offset, count);
        }

        protected override void UnreadImpl(char c)
        {
            Unread(new[] { c }, 0, 1);
        }

        protected override void UnreadImpl(char[] buffer, int offset, int count)
        {
            if (binaryReader.BaseStream.CanSeek)
            {
                var length = Encoding.GetByteCount(buffer, offset, count);
                binaryReader.BaseStream.Seek(-length, SeekOrigin.Current);
            }
            else
            {
                var pushbackBuffer = Encoding.GetBytes(buffer, offset, count);
                binaryReader.BaseStream.Write(pushbackBuffer, 0, pushbackBuffer.Length);
            }
        }
    }
}
