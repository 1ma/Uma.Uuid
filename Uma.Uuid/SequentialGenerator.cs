using System;
using System.Buffers.Binary;

namespace Uma.Uuid
{
    public class SequentialGenerator : IUuidGenerator
    {
        private ulong _counter;
        private readonly byte[] _bytes;

        public SequentialGenerator(ulong head = 0, ulong start = 0)
        {
            _counter = start;
            _bytes = new byte[2 * sizeof(ulong)];

            // Write head bytes to _bytes[0..7] once
            BinaryPrimitives.WriteUInt64BigEndian(_bytes.AsSpan(), head);
        }

        public Uuid NewUuid(string name = null)
        {
            var tail = new byte[sizeof(ulong)];

            BinaryPrimitives.WriteUInt64BigEndian(tail.AsSpan(), _counter);

            _counter++;

            // Copy tail bytes to _bytes[8..15]
            Array.Copy(tail, 0, _bytes, 8, 8);

            return new Uuid(_bytes);
        }
    }
}
