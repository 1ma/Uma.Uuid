using System;
using System.Linq;

namespace Uma.Uuid
{
    internal static class GuidAdapter
    {
        public static Guid FromByteArray(byte[] bytes)
        {
            var a = bytes.Take(4).Reverse().ToArray();
            var b = bytes.Skip(4).Take(2).Reverse().ToArray();
            var c = bytes.Skip(6).Take(2).Reverse().ToArray();

            return new Guid(
                BitConverter.ToInt32(a, 0),
                BitConverter.ToInt16(b, 0),
                BitConverter.ToInt16(c, 0),
                bytes[8],
                bytes[9],
                bytes[10],
                bytes[11],
                bytes[12],
                bytes[13],
                bytes[14],
                bytes[15]
            );
        }

        public static byte[] FromGuid(Guid guid)
        {
            var bytes = guid.ToByteArray();
            var result = new byte[16];

            var a = bytes.Take(4).Reverse().ToArray();
            var b = bytes.Skip(4).Take(2).Reverse().ToArray();
            var c = bytes.Skip(6).Take(2).Reverse().ToArray();
            var rest = bytes.Skip(8).ToArray();

            a.CopyTo(result, 0);
            b.CopyTo(result, 4);
            c.CopyTo(result, 6);
            rest.CopyTo(result, 8);

            return result;
        }
    }
}
