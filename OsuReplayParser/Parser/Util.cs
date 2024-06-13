using System;
using System.IO;
using System.Text;

namespace OsuReplayParser.Parser
{
    public static class Util
    {
        /// <summary>
        /// Reads a stream of bytes from an .osr file representing a string.
        /// Follows: https://osu.ppy.sh/wiki/en/Client/File_formats/osr_%28file_format%29
        /// </summary>
        /// <param name="br">BinaryReader instance to read from.</param>
        /// <returns>String value.</returns>
        public static string ReadOsrString(this BinaryReader br)
        {
            byte currentByte = br.ReadByte();

            switch (currentByte)
            {
                case 0:
                    return br.ReadString();
                case 11:
                    int stringLength = br.ReadULEB128();
                    byte[] stringBytes = br.ReadBytes(stringLength);
                    return Encoding.UTF8.GetString(stringBytes);
                default:
                    return "Expected the first byte to be 0x00 or 0x0b, but got " + currentByte.ToString();
            }
        }

        /// <summary>
        /// Reads an unsigned LEB128 value from the current stream, advancing the stream position.
        /// </summary>
        /// <param name="br"></param>
        /// <returns>Decoded ULEB128 value.</returns>
        public static int ReadULEB128(this BinaryReader br)
        {
            int val = 0;
            int shift = 0;
            byte currentByte;

            // Finish when most-significant bit is 0
            do
            {
                currentByte = br.ReadByte();
                val |= (currentByte & 0x7f) << shift;
                shift += 7;
            }
            while ((currentByte & 0x80) != 0);

            return val;
        }
    }
}
