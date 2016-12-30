using System;
using System.Drawing;
using System.IO;
using System.Text;
using ThoughtWorks.QRCode.Codec;
using ThoughtWorks.QRCode.Codec.Data;
using Utility.IO;

namespace Utility.Utils
{
    /// <summary>
    /// 二维码帮助类
    /// </summary>
    public class QRCodeHelper
    {
        /// <summary>
        /// 生成QRcode二维码
        /// </summary>
        /// <param name="url">要编码的字符串</param>
        /// <param name="path">保存路径</param>
        /// <returns>生成后的二维码图片</returns>
        public static string Create(string url, string path)
        {
            QRCodeEncoder qrEntity = new QRCodeEncoder
            {
                QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE,
                QRCodeScale = 10,
                QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M
            };

            System.Drawing.Bitmap srcimage = qrEntity.Encode(url, Encoding.UTF8);
          
            string filename = Guid.NewGuid().ToString().Replace("-", "") + ".jpg";
            FileHelper.CreateDirectory(path);
            srcimage.Save(string.Format("{0}/{1}", path, filename), System.Drawing.Imaging.ImageFormat.Jpeg);

            return filename;
        }

        /// <summary>
        /// 二维码解码
        /// </summary>
        /// <param name="filePath">图片路径</param>
        /// <returns></returns>
        public string CodeDecoder(string filePath)
        {
            if (!File.Exists(filePath))
                return null;
            Bitmap myBitmap = new Bitmap(Image.FromFile(filePath));
            QRCodeDecoder decoder = new QRCodeDecoder();
            string decodedString = decoder.decode(new QRCodeBitmapImage(myBitmap));
            return decodedString;
        }
    }
}
