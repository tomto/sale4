using System.IO;
using System.Text;

namespace Utility.Utils {

    public class StreamHelper {

        public static string ToString(Stream stream) {
            int count = 0;
            int length = 256;
            byte[] buffer = new byte[length];
            StringBuilder builder = new StringBuilder();

            while ((count = stream.Read(buffer, 0, length)) > 0) {
                builder.Append(Encoding.UTF8.GetString(buffer, 0, count));
            }

            stream.Flush();
            stream.Close();
            stream.Dispose();

            return builder.ToString();
        }
    }
}
