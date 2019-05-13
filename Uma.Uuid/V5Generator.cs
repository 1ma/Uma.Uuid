using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Uma.Uuid
{
    /// <summary>
    /// IGuidGenerator that returns a version 5 Guid.
    ///
    /// https://tools.ietf.org/html/rfc4122#section-4.3
    /// </summary>
    public class V5Generator : IGuidGenerator
    {
        /**
         * These are a few well known Guids listed in Appendix C
         * of RFC 4122 to be used as namespace identifiers.
         */
        public const string NS_DNS = "6ba7b810-9dad-11d1-80b4-00c04fd430c8";
        public const string NS_URL = "6ba7b811-9dad-11d1-80b4-00c04fd430c8";
        public const string NS_OID = "6ba7b812-9dad-11d1-80b4-00c04fd430c8";
        public const string NS_X500 = "6ba7b814-9dad-11d1-80b4-00c04fd430c8";

        private readonly SHA1 hasher;
        private readonly byte[] prefix;

        public V5Generator(Guid ns)
        {
            hasher = SHA1.Create();
            prefix = GuidAdapter.FromGuid(ns);
        }

        public Guid generate(string name = null)
        {
            var encoded = Encoding.UTF8.GetBytes(name);
            var input = new byte[16 + encoded.Length];
            prefix.CopyTo(input, 0);
            encoded.CopyTo(input, 16);

            var hash = hasher.ComputeHash(input);
            var bytes = hash.Take(16).ToArray();

            // Set the four most significant bits (bits 12 through 15) of
            // the time_hi_and_version field to the 4-bit version number from
            // Section 4.1.3. (these are 0100 for v4)
            bytes[6] &= 0b_0101_1111;
            bytes[6] |= 0b_0101_0000;

            // Set the two most significant bits (bits 6 and 7) of the
            // clock_seq_hi_and_reserved to zero and one, respectively.
            bytes[8] &= 0b_1011_1111;
            bytes[8] |= 0b_1000_0000;

            return GuidAdapter.FromByteArray(bytes);
        }
    }
}
