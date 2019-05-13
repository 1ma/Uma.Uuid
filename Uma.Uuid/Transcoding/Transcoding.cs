using System;
using System.Text;

namespace Uma.Uuid.Transcoding
{
    public static class Transcoding
    {
        private const byte ASCII_0 = 48;
        private const byte ASCII_9 = 57;
        private const byte ASCII_A = 65;
        private const byte ASCII_F = 70;
        private const byte ASCII_a = 97;
        private const byte ASCII_f = 102;

        public static string BinToHex(byte[] input)
        {
            var sb = new StringBuilder(2 * input.Length);

            foreach (var b in input)
            {
                sb.Append(lol(b));
            }

            return sb.ToString();
        }

        public static byte[] HexToBin(string input)
        {
            if (input.Length % 2 != 0)
            {
                throw new ArgumentException("dafuq is this");
            }

            var bytes = new byte[input.Length / 2];

            for (var i = 0; i < bytes.Length; i++)
            {
                bytes[i] = (byte) (16 * derp(input[2 * i]) + derp(input[2 * i + 1]));
            }

            return bytes;
        }

        private static string lol(byte b)
        {
            var hi = (byte) (b / 16);
            var lo = (byte) (b % 16);

            return new string(new[] {antiDerp(hi), antiDerp(lo)});
        }

        private static int derp(char hb)
        {
            var cast = (byte) hb;

            if (ASCII_0 <= cast && cast <= ASCII_9)
            {
                return cast - ASCII_0;
            }

            if (ASCII_A <= cast && cast <= ASCII_F)
            {
                return cast - ASCII_A + 10;
            }

            if (ASCII_a <= cast && cast <= ASCII_f)
            {
                return cast - ASCII_a + 10;
            }

            throw new ArgumentException("fuck this input");
        }

        private static char antiDerp(byte b)
        {
            if (0 <= b && b <= 9)
            {
                return (char) (b + ASCII_0);
            }

            return (char) (b + ASCII_a - 10);
        }
    }
}