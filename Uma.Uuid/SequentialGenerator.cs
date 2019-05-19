using System;
using System.Buffers.Binary;

namespace Uma.Uuid
{
    /// <summary>
    /// A deterministic generator for testing scenarios. With the default configuration
    /// it will generate the NIL Uuid (00000000-0000-0000-0000-000000000000), then
    /// 00000000-0000-0000-0000-000000000001, 00000000-0000-0000-0000-000000000002... etc.
    /// </summary>
    public class SequentialGenerator : IUuidGenerator
    {
        private ulong _counter;
        private readonly byte[] _bytes;

        /// <param name="head">Set a custom start other than 0000-000000000000</param>
        /// <param name="start">Set a custom higher 8 bytes other than 00000000-0000-0000</param>
        /// <remarks>
        /// Pass the head in hexadecimal form (such as 0xabad1dea) to see how it will be printed.
        /// </remarks>
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
