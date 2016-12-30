using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utility.Utils {

    public class NumericHelper {

        public enum Type {
            /// <summary>
            /// 纯数字
            /// </summary>
            number,

            /// <summary>
            /// 小写字母
            /// </summary>
            lowercase,

            /// <summary>
            /// 大写字母
            /// </summary>
            uppercase,

            /// <summary>
            /// 数字和大写字母
            /// </summary>
            numberAndUppercase,

            /// <summary>
            /// 全部
            /// </summary>
            all
        }

        /// <summary>
        /// 用于生成随机码的字符集。
        /// </summary>
        private static char[] chars = { 
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
            'a', 'b', 'c', 'd', 'e', 'f' ,'g', 'h', 'i', 'j', 
            'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 
            'u', 'v', 'w', 'x', 'y', 'z',
            'A', 'B', 'C', 'D', 'E', 'F' ,'G', 'H', 'I', 'J',
            'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T',
            'U', 'V', 'W', 'X', 'Y', 'Z', 
            '_'
        };

        /// <summary>
        /// 数字数量
        /// </summary>
        private const int NUMBER_COUNT = 10;

        /// <summary>
        /// 英文字符数量
        /// </summary>
        private const int LETTER_COUNT = 26;

        /// <summary>
        /// 获取对应类型在随机码字符集中的长度
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static int GetTypeCharLenght(Type type) {
            int charLength = 0;

            switch (type) {
                case Type.number:
                    charLength = NUMBER_COUNT;
                    break;
                case Type.lowercase:
                case Type.uppercase:
                    charLength = LETTER_COUNT;
                    break;
                case Type.numberAndUppercase:
                    charLength = NUMBER_COUNT + LETTER_COUNT;
                    break;
                case Type.all:
                    charLength = chars.Length;
                    break;
            }

            return charLength;
        }

        private static int getTypeCharStartIndex(Type type) {
            int startIndex = 0;

            switch (type) {
                case Type.number:
                case Type.all:
                    startIndex = 0;
                    break;
                case Type.lowercase:
                    startIndex = NUMBER_COUNT;
                    break;
                case Type.uppercase:
                    startIndex = NUMBER_COUNT + LETTER_COUNT;
                    break;
            }

            return startIndex;
        }

        private static char[] getSubChars(Type type) {
            List<char> charList = chars.ToList();
            int startIndex = 0;
            int charLength = 0;
            List<char> subList = new List<char>();

            if (type == Type.numberAndUppercase) {
                startIndex = getTypeCharStartIndex(Type.number);
                charLength = GetTypeCharLenght(Type.number);
                List<char> numberCharList = charList.GetRange(startIndex, charLength);

                startIndex = getTypeCharStartIndex(Type.uppercase);
                charLength = GetTypeCharLenght(Type.uppercase);
                List<char> uppercaseCharList = charList.GetRange(startIndex, charLength);

                subList.AddRange(numberCharList);
                subList.AddRange(uppercaseCharList);
            } else {
                startIndex = getTypeCharStartIndex(type);
                charLength = GetTypeCharLenght(type);
                subList = charList.GetRange(startIndex, charLength);
            }
            
            return subList.ToArray();
        }

        /// <summary>
        /// 生成随机码，内容包括数字、大小写英文字母、下划线
        /// </summary>
        /// <param name="length">密码长度</param>
        /// <returns>随机6位密码</returns>
        public static string Random(Type type, int length) {
            int charLength = GetTypeCharLenght(type);
            StringBuilder code = new StringBuilder(charLength);
            Random rdm = new Random();
            char[] subChars = getSubChars(type);

            for (int i = 0; i < length; ++i) {
                code.Append(subChars[rdm.Next(charLength)]);
            }

            return code.ToString();
        }
    }
}
