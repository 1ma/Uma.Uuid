using System;
using System.Security.Cryptography;
using System.Text;

namespace Uma.Uuid
{
    /// <summary>
    /// IGuidGenerator that returns a version 5 Guid.
    ///
    /// https://tools.ietf.org/html/rfc4122#section-4.3
    /// </summary>
    public class V5Generator : IUuidGenerator
    {
        /**
         * These are a few well known Guids listed in Appendix C
         * of RFC 4122 to be used as namespace identifiers.
         */
        public const string NS_DNS = "6ba7b810-9dad-11d1-80b4-00c04fd430c8";
        public const string NS_URL = "6ba7b811-9dad-11d1-80b4-00c04fd430c8";
        public const string NS_OID = "6ba7b812-9dad-11d1-80b4-00c04fd430c8";
        public const string NS_X500 = "6ba7b814-9dad-11d1-80b4-00c04fd430c8";

        private readonly SHA1 _hasher;
        private readonly byte[] _hi;

        public V5Generator(Uuid ns)
        {
            _hasher = SHA1.Create();
            _hi = ns.ToByteArray();
        }

        public Uuid Generate(string name = null)
        {
            if (name == null)
            {
                throw new ArgumentException("name is mandatory. Got: null");
            }

            var lo = Encoding.UTF8.GetBytes(name);
            var input = new byte[16 + lo.Length];

            Array.Copy(_hi, input, 16);
            Array.Copy(lo, 0, input, 16, lo.Length);

            var hash = _hasher.ComputeHash(input);
            var bytes = new byte[16];

            Array.Copy(hash, 0, bytes, 0, 16);

            // Set the four most significant bits (bits 12 through 15) of
            // the time_hi_and_version field to the 4-bit version number from
            // Section 4.1.3. (these are 0100 for v4)
            bytes[6] &= 0b_0101_1111;
            bytes[6] |= 0b_0101_0000;

            // Set the two most significant bits (bits 6 and 7) of the
            // clock_seq_hi_and_reserved to zero and one, respectively.
            bytes[8] &= 0b_1011_1111;
            bytes[8] |= 0b_1000_0000;

            return new Uuid(bytes);
        }
    }
}
