using System;
using System.Text.RegularExpressions;
using Uma.Uuid.Transcoding;

namespace Uma.Uuid
{
    /// <summary>
    /// Value object that encapsulates the 128 bits of an UUID.
    /// </summary>
    public class Uuid
    {
        private readonly byte[] _uuid;

        private static readonly Regex _regex = new Regex(
            @"^[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}$",
            RegexOptions.Compiled | RegexOptions.IgnoreCase
        );

        public static bool IsUuid(string text)
        {
            return text.Length == 36 && _regex.IsMatch(text);
        }

        public Uuid(string text)
        {
            if (!IsUuid(text))
            {
                throw new ArgumentException($"text is not a valid Uuid. Got: {text}");
            }

            _uuid = Transcoder.HexToBin(text.Replace("-", string.Empty));
        }

        public Uuid(byte[] bytes)
        {
            if (bytes.Length != 16)
            {
                throw new ArgumentException($"Length of bytes for new Uuid is not 16. Got: {bytes.Length.ToString()}");
            }

            _uuid = bytes;
        }

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

        public byte[] ToByteArray()
        {
            return _uuid;
        }

        public Guid ToGuid()
        {
            return new Guid(ToString());
        }
    }
}
