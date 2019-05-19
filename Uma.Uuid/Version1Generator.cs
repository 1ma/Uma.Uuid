using System;
using System.Buffers.Binary;
using System.Text.RegularExpressions;
using Uma.Uuid.Transcoding;

namespace Uma.Uuid
{
    /// <summary>
    /// A Version 1 Uuid generator, as described in RFC 4122.
    /// </summary>
    public class Version1Generator : IUuidGenerator
    {
        /// <summary>
        /// This is the number of 100-nanosecond intervals elapsed
        /// from 0001-01-01 00:00:00 UTC to 1582-10-15 OO:OO:OO UTC.
        /// </summary>
        /// <remarks>
        /// The algorithm to generate Version 1 Uuids uses the number of 100-nanosecond
        /// intervals elapsed since the introduction of the Gregorian calendar in 1582 to
        /// fill the higher bits of the Uuid. In order to do so Version1Generator
        /// subtracts this constant to the result of calling DateTimeOffset.Now.Ticks.
        ///
        /// The following is an informal proof of correctness:
        ///
        /// DateTimeOffset gregorian = new DateTimeOffset(499163040000000000, DateTimeOffset.Now.Offset);
        /// Console.WriteLine(gregorian.ToString("yyyy-MM-dd HH:mm:ss.ffffff"));
        ///
        /// Output: 1582-10-15 00:00:00.000000
        /// </remarks>
        private const long GregorianOffset = 499163040000000000;

        private static readonly Regex _regex = new Regex(
            @"^[0-9a-f]{2}:[0-9a-f]{2}:[0-9a-f]{2}:[0-9a-f]{2}:[0-9a-f]{2}:[0-9a-f]{2}$",
            RegexOptions.Compiled | RegexOptions.IgnoreCase
        );

        private readonly Random _random = new Random();

        private readonly byte[] _nodeId;

        public Version1Generator(string nodeId)
        {
            if (nodeId.Length != 17 || !_regex.IsMatch(nodeId))
            {
                throw new ArgumentException($"nodeId is not a valid MAC address. Got: {nodeId}");
            }

            _nodeId = Transcoder.HexToBin(nodeId.Replace(":", string.Empty));
        }

        public Uuid NewUuid(string name = null)
        {
            ulong timestamp = (ulong) DateTimeOffset.Now.Ticks - GregorianOffset;

            var bytes = new byte[16];

            BinaryPrimitives.WriteUInt32BigEndian(bytes.AsSpan().Slice(0, 4), (uint) timestamp & 0xffffffff);
            BinaryPrimitives.WriteUInt16BigEndian(bytes.AsSpan().Slice(4, 2), (ushort) (timestamp >> 32 & 0xffff));
            BinaryPrimitives.WriteUInt16BigEndian(bytes.AsSpan().Slice(6, 2), (ushort) (timestamp >> 48 & 0x0fff | 0x1000));
            BinaryPrimitives.WriteUInt16BigEndian(bytes.AsSpan().Slice(8, 2), (ushort) (_random.Next(0, 0x3fff) | 0x8000));

            Array.Copy(_nodeId, 0, bytes, 10, 6);

            return new Uuid(bytes);
        }
    }
}
