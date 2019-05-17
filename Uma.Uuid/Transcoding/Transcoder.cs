using System;
using System.Text;

namespace Uma.Uuid.Transcoding
{
    /// <summary>
    /// The Transcoding class provides a C# implementation of the bin2hex() and hex2bin() PHP functions.
    /// </summary>
    public static class Transcoder
    {
        private const byte ASCII_0 = 48;
        private const byte ASCII_9 = 57;
        private const byte ASCII_A = 65;
        private const byte ASCII_F = 70;
        private const byte ASCII_a = 97;
        private const byte ASCII_f = 102;

        /// <summary>
        /// Turns a byte array into a hex-encoded string following big-endian order.
        /// </summary>
        public static string BinToHex(byte[] input)
        {
            var sb = new StringBuilder(2 * input.Length);

            foreach (var b in input)
            {
                var hi = (byte) (b / 16);
                var lo = (byte) (b % 16);

                sb.Append(HexEncode(hi));
                sb.Append(HexEncode(lo));
            }

            return sb.ToString();
        }

        /// <summary>
        /// Decodes a hex-encoded string into a byte array following big-endian order.
        /// </summary>
        /// <exception cref="ArgumentException">If input length is not even, or it contains a non hexadecimal character.</exception>
        public static byte[] HexToBin(string input)
        {
            if (input.Length % 2 != 0)
            {
                throw new ArgumentException("dafuq is this");
            }

            var bytes = new byte[input.Length / 2];

            for (var i = 0; i < bytes.Length; i++)
            {
                bytes[i] = (byte) (16 * HexDecode(input[2 * i]) + HexDecode(input[2 * i + 1]));
            }

            return bytes;
        }

        /// <summary>
        /// Decodes an ASCII character to a half-byte.
        /// </summary>
        /// <param name="hb">An ASCII character representing a half-byte.</param>
        /// <returns>An integer between 0 and 15.</returns>
        /// <exception cref="ArgumentException">If hb does not fall in one of the [0-9], [A-F] or [a-f] ranges.</exception>
        private static byte HexDecode(char hb)
        {
            var cast = (byte) hb;

            if (ASCII_0 <= cast && cast <= ASCII_9)
            {
                return (byte)(cast - ASCII_0);
            }

            if (ASCII_A <= cast && cast <= ASCII_F)
            {
                return (byte)(cast - ASCII_A + 10);
            }

            if (ASCII_a <= cast && cast <= ASCII_f)
            {
                return (byte)(cast - ASCII_a + 10);
            }

            throw new ArgumentException("fuck this input");
        }

        /// <summary>
        /// Encodes a half-byte into an ASCII character.
        /// </summary>
        /// <param name="b">A byte between 0 and 15.</param>
        /// <returns>A character in the [0-9] or [a-f] ranges.</returns>
        private static char HexEncode(byte b)
        {
            if (b <= 9)
            {
                return (char) (b + ASCII_0);
            }

            return (char) (b + ASCII_a - 10);
        }
    }
}
