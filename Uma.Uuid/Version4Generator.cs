using System;

namespace Uma.Uuid
{
    /// <summary>
    /// IGuidGenerator that returns a version 4 Guid.
    ///
    /// https://tools.ietf.org/html/rfc4122#section-4.4
    /// </summary>
    public class Version4Generator : IUuidGenerator
    {
        private readonly Random _random = new Random();

        public Uuid NewUuid(string name = null)
        {
            var bytes = new byte[16];

            _random.NextBytes(bytes);

            // Set the four most significant bits (bits 12 through 15) of
            // the time_hi_and_version field to the 4-bit version number from
            // Section 4.1.3. (these are 0100 for v4)
            bytes[6] &= 0b_0100_1111;
            bytes[6] |= 0b_0100_0000;

            // Set the two most significant bits (bits 6 and 7) of the
            // clock_seq_hi_and_reserved to zero and one, respectively.
            bytes[8] &= 0b_1011_1111;
            bytes[8] |= 0b_1000_0000;

            return new Uuid(bytes);
        }
    }
}
