using System;
using System.Text.RegularExpressions;
using Uma.Uuid.Transcoding;

namespace Uma.Uuid
{
    /// <summary>
    /// Value object that encapsulates the 128 bits of an UUID.
    /// </summary>
    public struct Uuid
    {
        private readonly byte[] _uuid;

        /// <summary>
        /// This is the regular expression that is used to check that
        /// an string is a valid representation of an Uuid.
        /// </summary>
        private static readonly Regex _regex = new Regex(
            @"^[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}$",
            RegexOptions.Compiled | RegexOptions.IgnoreCase
        );

        /// <summary>
        /// Check if an arbitrary string is a legally formatted Uuid
        /// without throwing an exception.
        /// </summary>
        public static bool IsUuid(string text)
        {
            return text.Length == 36 && _regex.IsMatch(text);
        }

        /// <summary>
        /// Instantiate an Uuid from a string.
        /// </summary>
        /// <exception cref="ArgumentException">If text is not a valid string representation of an Uuid.</exception>
        public Uuid(string text)
        {
            if (!IsUuid(text))
            {
                throw new ArgumentException($"text is not a valid Uuid. Got: {text}");
            }

            _uuid = Transcoder.HexToBin(text.Replace("-", string.Empty));
        }

        /// <summary>
        /// Instantiate an Uuid from an array of 16 bytes.
        /// </summary>
        /// <exception cref="ArgumentException">If bytes is not exactly 16 elements long.</exception>
        public Uuid(byte[] bytes)
        {
            if (bytes.Length != 16)
            {
                throw new ArgumentException($"Length of bytes for new Uuid is not 16. Got: {bytes.Length.ToString()}");
            }

            _uuid = bytes;
        }

        /// <summary>
        /// Serialize the Uuid into a big-endian string.
        /// </summary>
        public override string ToString()
        {
            var tmp = Transcoder.BinToHex(_uuid);

            return string.Format(
                "{0}-{1}-{2}-{3}-{4}",
                tmp.Substring(0, 8),
                tmp.Substring(8, 4),
                tmp.Substring(12, 4),
                tmp.Substring(16, 4),
                tmp.Substring(20, 12)
            );
        }

        /// <summary>
        /// Serialize the Uuid into a big-endian array of 16 bytes.
        /// </summary>
        public byte[] ToByteArray()
        {
            return _uuid;
        }

        /// <summary>
        /// Instantiate an equivalent Guid.
        /// </summary>
        /// <remarks>
        /// Calling Guid.ToString() will yield the same result than Uuid.ToString(),
        /// but Guid.ToByteArray() will not!
        /// </remarks>
        public Guid ToGuid()
        {
            return new Guid(ToString());
        }
    }
}
