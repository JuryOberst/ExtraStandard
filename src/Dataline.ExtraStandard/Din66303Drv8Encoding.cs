using System;
using System.Collections.Generic;
using System.Text;

namespace ExtraStandard
{
    /// <summary>
    /// DIN 66303 in der Fassung von 1986-11 (Deutsche Referenz-Version des 8-Bit-Code (DRV8))
    /// </summary>
    public class Din66303Drv8Encoding : Encoding
    {
        private static readonly char?[] _bytesToChars = {
                null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null,
                null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null,
                ' ', '!', '"', '#', '$', '%', '&', '\'', '(', ')', '*', '+', ',', '-', '.', '/',
                '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', ':', ';', '<', '=', '>', '?',
                '§', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O',
                'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'Ä', 'Ö', 'Ü', '^', '_',
                '`', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o',
                'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'ä', 'ö', 'ü', 'ß', null,
                null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null,
                null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null,
                '\u00a0', 'Í', '¢', '£', '¤', '¥', '¦', '@', '¨', '©', 'ª', '«', '¬', '\u00ad', '®', '¯',
                '°', '±', '²', '³', '´', 'µ', '¶', '·', '¸', '¹', 'º', '»', '¼', '½', '¾', '¿',
                'À', 'Á', 'Â', 'Ã', '[', 'Å', 'Æ', 'Ç', 'È', 'É', 'Ê', 'Ë', 'Ì', 'Í', 'Î', 'Ï',
                'Ð', 'Ñ', 'Ò', 'Ó', 'Ô', 'Ö', '\\', '×', 'Ø', 'Ù', 'Ú', 'Û', ']', 'Ý', 'Þ', '~',
                'à', 'á', 'â', 'ä', '{', 'å', 'æ', 'ç', 'è', 'é', 'ê', 'ë', 'ì', 'í', 'î', 'ï',
                'ð', 'ñ', 'ò', 'ó', 'ô', 'ö', '|', '÷', 'ø', 'ù', 'ú', 'û', '}', 'ý', 'þ', 'ÿ'
            };

        private static readonly Lazy<IDictionary<char, byte>> _charsToBytes = new Lazy<IDictionary<char, byte>>(() =>
        {
            var result = new Dictionary<char, byte>(256);
            for (var i = 0; i != _bytesToChars.Length; ++i)
            {
                var ch = _bytesToChars[i];
                if (ch.HasValue)
                {
                    switch (i)
                    {
                        case 161:
                        case 213:
                        case 227:
                        case 245:
                            break;
                        default:
                            result.Add(ch.Value, (byte)i);
                            break;
                    }
                }
            }
            return result;
        });

        /// <inheritdoc />
        public override string EncodingName { get; } = "DIN 66303:1986-11 DRV8";

        /// <inheritdoc />
        public override bool IsSingleByte { get; } = true;

        /// <inheritdoc />
        public override int GetByteCount(char[] chars, int index, int count)
        {
            var result = 0;
            var charsToBytes = _charsToBytes.Value;
            for (var i = 0; i != count; ++i)
            {
                if (charsToBytes.ContainsKey(chars[index + i]))
                {
                    result += 1;
                }
            }
            return result;
        }

        /// <inheritdoc />
        public override int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex)
        {
            var charsToBytes = _charsToBytes.Value;
            var destIndex = byteIndex;
            for (var i = 0; i != charCount; ++i)
            {
                byte b;
                var c = chars[charIndex + i];
                if (charsToBytes.TryGetValue(c, out b))
                {
                    bytes[destIndex++] = b;
                }
            }
            return destIndex - byteIndex;
        }

        /// <inheritdoc />
        public override int GetCharCount(byte[] bytes, int index, int count)
        {
            var result = 0;
            for (var i = 0; i != count; ++i)
            {
                var b = bytes[index + i];
                var c = _bytesToChars[b];
                if (c.HasValue)
                {
                    result += 1;
                }
            }
            return result;
        }

        /// <inheritdoc />
        public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
        {
            var destIndex = charIndex;
            for (var i = 0; i != byteCount; ++i)
            {
                var c = _bytesToChars[bytes[byteIndex + i]];
                if (c.HasValue)
                {
                    chars[destIndex++] = c.Value;
                }
            }
            return destIndex - charIndex;
        }

        /// <inheritdoc />
        public override int GetMaxByteCount(int charCount)
        {
            return charCount;
        }

        /// <inheritdoc />
        public override int GetMaxCharCount(int byteCount)
        {
            return byteCount;
        }
    }
}
