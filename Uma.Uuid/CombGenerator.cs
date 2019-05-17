using System;
using System.Buffers.Binary;

namespace Uma.Uuid
{
    public class CombGenerator : IUuidGenerator
    {
        private const long UnixEpoch = 621355968000000000;

        private readonly IUuidGenerator _version4 = new Version4Generator();

        public Uuid NewUuid(string name = null)
        {
            var tmp = new byte[8];
            var timestamp = DateTimeOffset.Now.Ticks - UnixEpoch;
            BinaryPrimitives.WriteInt64BigEndian(tmp.AsSpan(), (timestamp / 10) << 4);

            var bytes = new byte[16];
            Array.Copy(tmp, 1, bytes, 0, 6);
            Array.Copy(_version4.NewUuid().ToByteArray(), 6, bytes, 6, 10);

            return new Uuid(bytes);
        }
    }
}
