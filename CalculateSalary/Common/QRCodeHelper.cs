//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Drawing;
//using System.IO;
//using System.Text;
//using ZXing.QrCode;


//namespace ElecReceipt.Common
//{
//    /// <summary>
//    /// 二维码生成工具
//    /// </summary>
//    public class QRCodeHelper
//    {
//        /// <summary>
//        /// 得到二维码的Base64数据
//        /// </summary>
//        /// <param name="msg"></param>
//        /// <param name="width"></param>
//        /// <param name="height"></param>
//        /// <returns></returns>
//        public static string GetBase64QRCode(string msg, int width, int height)
//        {
//            var qrCodeWriter = new ZXing.BarcodeWriterPixelData
//            {
//                Format = ZXing.BarcodeFormat.QR_CODE,
//                Options = new QrCodeEncodingOptions
//                {
//                    Height = height,
//                    Width = width,
//                    Margin = 1
//                }
//            };
//            var pixelData = qrCodeWriter.Write(msg);
//            // creating a bitmap from the raw pixel data; if only black and white colors are used it makes no difference   
//            // that the pixel data ist BGRA oriented and the bitmap is initialized with RGB   
//            using (var bitmap = new System.Drawing.Bitmap(pixelData.Width, pixelData.Height,
//                System.Drawing.Imaging.PixelFormat.Format32bppRgb))
//            using (var ms = new MemoryStream())
//            {
//                var bitmapData = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, pixelData.Width, pixelData.Height),
//                    System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
//                try
//                {
//                    // we assume that the row stride of the bitmap is aligned to 4 byte multiplied by the width of the image   
//                    System.Runtime.InteropServices.Marshal.Copy(pixelData.Pixels, 0, bitmapData.Scan0,
//                        pixelData.Pixels.Length);
//                }
//                finally
//                {
//                    bitmap.UnlockBits(bitmapData);
//                }
//                // save to stream as PNG   
//                bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
//                return Convert.ToBase64String(ms.ToArray());
//                //byte[] arr = Convert.FromBase64String(base64);
//                //var ms1 = new MemoryStream(arr);
//                //var bmp = new Bitmap(ms1);
//                //bmp.Save(@"c:\test1.png", System.Drawing.Imaging.ImageFormat.Png);
//                //ms1.Close();
                
//            }
//        }
//    }
//}
