namespace Uma.Uuid
{
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

        public Uuid Generate(string name = null)
        {
            return new Uuid("");
        }
    }
}
