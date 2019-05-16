using System;
using System.Buffers.Binary;

namespace Uma.Uuid
{
    public class CombGenerator : IUuidGenerator
    {
        private readonly IUuidGenerator _version4 = new Version4Generator();

        public Uuid NewUuid(string name = null)
        {
            var bytes = new byte[16];

            BinaryPrimitives.WriteUInt64BigEndian(bytes.AsSpan(), (ulong) DateTimeOffset.Now.Ticks);

            Array.Copy(_version4.NewUuid().ToByteArray(), 6, bytes, 6, 10);

            return new Uuid(bytes);
        }
    }
}
