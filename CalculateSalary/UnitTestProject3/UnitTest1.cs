using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mysoft.Tjqxj.Models;
using Mysoft.Tjqxj.Models.ViewModel;
using Mysoft.Tjqxj.Services;

namespace UnitTestProject3
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            //var temp = new FeedBackTemplate();
            //temp.FeedbackTime.Value=DateTime.Now.ToString();
            //temp.Msg.Value = "十分感谢";
            //temp.Title.Value = "我是标题";
            //WxCommonService.SendMsgToUser("ofbAr1AtymN6KJS7dnzrCyNcJq30", "U_H9C_qev8jAh6iKQYmbnbSpO5RBjt4G0ZWqIXYZ6xk",
            //    temp);
        }

        [TestMethod]
        public void GetTemplateId()
        {
            var id =WxCommonService.GetTemplateId("手动");
        }

        [TestMethod]
        public void GetRealTime()
        {
             var result= CaiyunService.GetRealtimeInfo("104.06476", "30.5702");
        }

        [TestMethod]
        public void GetPreInfo()
        {
            var result =CaiyunService.GetPrediction(new RealtimeClimateInfo(), "104.06476", "30.5702");
        }

        [TestMethod]
        public void GetImageInfo()
        {
             //SatelliteLiveService.GetImageInfo();
        }

        [TestMethod]
        public void CombineStr()
        {
            var temp = @"　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　
　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　
　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　
　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　
　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　
　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　
　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　
　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　
　　　　　　　　　　　　ｘｘ　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　ｘｘ　　　　　　　　　　ｘｘ　　　　　　　　　　　　　　　　　　　
　　　　　　　　　　　　ｘｘｘ　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　ｘｘ　　　　　　　　　　　ｘｘｘ　　　　　　　　　　　　　　　　　
　　　　　　　　　　　　ｘｘｘ　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　ｘｘｘ　　　　　　　　　　ｘｘｘ　　　　　　　　　　　　　　　　　
　　　　　　　　　　　　ｘｘｘ　　　　　　　　　　　　　　ｘｘｘ　　　　　　　　　　　　　　　　　　　　　　　　　ｘｘｘ　　　　　　　　　　ｘｘｘ　　　　　　　　　　　　　　　　　
　　　　　　　　　　　　　ｘｘ　　　　　　　　　　　ｘｘｘｘｘｘ　　　　　　　　　　　　　　　　　　　　　　　　ｘｘｘ　　　　　　　　　　　ｘｘｘ　　　　　　　　　　　　　　　　　
　　　　　　　　　　　　　ｘｘ　　　ｘｘｘｘｘｘｘｘｘｘｘｘｘｘ　　　　　　　　　　　　　　　　　　　　　　　　ｘｘｘ　　　　　　　　　　　ｘｘｘ　　　ｘｘｘ　　　　　　　　　　　
　　　　　　　　　　　　ｘｘｘ　　　　ｘｘｘｘｘｘｘｘｘｘｘｘ　　　　　　　　　　　　　　　　　　　　　　　　ｘｘｘ　　　　　　　　　　　　ｘｘｘｘｘｘｘｘｘ　　　　　　　　　　　
　　　　　　　　　　　　ｘｘｘ　　　　　ｘｘ　　　　　ｘｘｘ　　　　　　　　　　　　　　　　　　　　　　　　　ｘｘ　　　　　　　　ｘｘｘｘｘｘｘｘｘｘｘｘｘｘ　　　　　　　　　　　
　　　　　　　　　　　　ｘｘｘ　　　　　　　　　　　ｘｘｘ　　　　　　　　　　　　　　　　　　　　　　　　　ｘｘｘ　　　　　ｘｘ　　ｘｘｘｘｘｘｘｘｘ　ｘｘ　　　　　　　　　　　　
　　　　　　　　　　　　ｘｘｘ　　　　　　　　　　ｘｘｘ　　　　　　　　　　　　　　　　　　　　　　　　　ｘｘｘ　　　　ｘｘｘｘｘ　　ｘ　　ｘｘｘ　　　ｘｘ　ｘｘｘｘ　　　　　　　
　　　　　　　　　　　　ｘｘｘ　ｘｘｘｘ　　　　ｘｘｘ　　　　　　　　　　　　　　　　　　　　　　　　　　ｘｘｘｘｘｘｘｘｘｘｘｘ　　　　　ｘｘｘｘｘｘｘｘｘｘｘｘｘ　　　　　　　
　　　　　　　　　ｘｘｘｘｘｘｘｘｘｘｘｘ　　ｘｘｘｘ　　　　　　　　　　　　　　　　　　　　　　　　　ｘｘｘｘ　ｘｘｘｘｘｘｘｘｘｘｘｘｘｘｘｘｘｘｘｘｘｘｘｘｘｘ　　　　　　　
　　　　　ｘｘｘｘｘｘｘｘｘｘｘｘｘｘ　　　ｘｘｘｘ　　　　　　　　　　　　　　　　　　　　　　　　　ｘｘｘｘｘ　　　　　ｘｘ　ｘｘｘｘｘｘｘｘｘｘ　ｘｘｘ　　　　　　　　　　　　
　　　　　　ｘｘｘｘｘｘｘｘｘ　　　　　　　ｘｘｘ　　　　　　　　　ｘｘｘ　　　　　　　　　　　　　　ｘｘｘｘｘ　　　　ｘｘｘ　　　ｘ　　　ｘｘｘ　　ｘｘｘ　　　　　　　　　　　　
　　　　　　　ｘ　　　ｘｘｘｘ　　　　　　ｘｘｘ　　　　　　　ｘｘｘｘｘｘｘ　　　　　　　　　　　　ｘｘｘｘｘｘ　　　　ｘｘ　　　　　　　　ｘｘｘｘｘｘｘｘ　　　　　　　　　　　　
　　　　　　　　　　　ｘｘｘｘｘ　　　　ｘｘｘｘｘｘｘｘｘｘｘｘｘｘｘｘｘ　　　　　　　　　　　　ｘｘｘ　ｘｘｘ　　　ｘｘｘ　　　　ｘｘｘｘｘｘｘｘｘｘｘｘｘ　　　　　　　　　　　
　　　　　　　　　　ｘｘｘｘｘｘｘｘｘｘｘｘｘｘｘｘｘｘｘｘｘｘｘｘｘｘ　　　　　　　　　　　　　ｘｘ　　ｘｘｘ　　　ｘｘｘ　ｘｘ　ｘｘｘｘｘｘｘｘｘｘ　　　　　　　　　　　　　　
　　　　　　　　　　ｘｘｘｘｘｘｘｘ　ｘｘｘｘｘｘｘｘ　ｘｘｘ　　ｘｘｘ　　　　　　　　　　　　ｘｘ　　　ｘｘｘ　　ｘｘｘ　ｘｘｘｘ　ｘ　　ｘｘｘ　　　　　　　　　　　　　　　　　
　　　　　　　　　ｘｘｘ　ｘｘ　ｘｘ　　ｘ　　ｘｘｘ　　ｘｘｘｘ　ｘｘｘ　　　　　　　　　　　　ｘ　　　　ｘｘｘ　ｘｘｘｘｘｘｘｘｘ　　　　ｘｘｘ　ｘｘｘｘ　　　　　　　　　　　　
　　　　　　　　　ｘｘｘ　ｘｘ　　ｘ　　　　　ｘｘｘ　ｘｘｘｘ　　ｘｘｘ　　　　　　　　　　　　　　　　　ｘｘｘ　　ｘｘｘｘｘｘｘ　ｘｘｘｘｘｘｘｘｘｘｘｘｘ　　　　　　　　　　　
　　　　　　　　ｘｘｘ　ｘｘｘ　　　　　　　ｘｘｘ　　ｘｘｘ　　　ｘｘ　　　　　　　　　　　　　　　　　　ｘｘｘ　　ｘｘ　　ｘｘｘｘｘｘｘｘｘｘｘｘｘｘｘｘｘ　　　　　　　　　　　
　　　　　　　　ｘｘ　　ｘｘｘ　　　　　　　ｘｘｘ　　ｘｘｘ　　　ｘｘ　　　　　　　　　　　　　　　　　　ｘｘｘ　　　　　　ｘｘｘ　ｘｘｘｘｘｘｘ　　　　　　　　　　　　　　　　　
　　　　　　　ｘｘｘ　　ｘｘｘ　　　　　　ｘｘｘ　　ｘｘｘ　　　ｘｘｘ　　　　　　　　　　　　　　　　　　ｘｘｘ　　　　　　ｘｘｘ　　　　　ｘｘｘ　　　　ｘｘｘ　　　　　　　　　　
　　　　　　ｘｘｘ　　　ｘｘｘ　　　　　ｘｘｘ　　　ｘｘ　　　　ｘｘｘ　　　　　　　　　　　　　　　　　　ｘｘｘ　　　　　　ｘｘ　　　　　ｘｘｘｘｘｘｘｘｘｘｘｘ　　　　　　　　　
　　　　　　ｘｘ　　　　ｘｘｘ　　　　　ｘｘｘ　　ｘｘｘ　　　　ｘｘｘ　　　　　　　　　　　　　　　　　　ｘｘｘ　ｘｘｘ　　ｘｘｘｘｘｘｘｘｘｘｘｘｘｘｘｘｘｘｘ　　　　　　　　　
　　　　　ｘｘｘ　　　　ｘｘｘ　　　　ｘｘｘ　　ｘｘｘ　　　　　ｘｘ　　　　　　　　　　　　　　　　　　　ｘｘｘ　ｘｘｘｘｘｘｘ　ｘｘｘｘｘｘｘｘ　　　　　　　　　　　　　　　　　
　　　　ｘｘｘ　　　　　ｘｘｘ　　　ｘｘｘ　　　ｘｘｘ　　　　ｘｘｘ　　　　　　　　　　　　　　　　　　　ｘｘｘ　　　ｘｘｘｘｘ　　　　　　ｘｘｘ　　　　　　　　　　　　　　　　　
　　　　ｘｘ　　　　　　ｘｘｘ　　ｘｘｘ　　　ｘｘｘ　　　　　ｘｘｘ　　　　　　　　　　　　　　　　　　　ｘｘｘ　　　　ｘｘｘｘｘ　　　　　ｘｘｘ　　　　　　　　　　　　　　　　　
　　　　ｘ　　　　　　　ｘｘｘ　ｘｘｘ　　　ｘｘｘ　　　　　　ｘｘ　　　　　　　　　　　　　　　　　　　　ｘｘｘ　　　　ｘｘｘｘｘｘｘｘ　　ｘｘｘ　　　　　　　　　　　　　　　　　
　　　　　　　　　　　　ｘｘｘｘｘｘ　　　　ｘｘｘ　　　　　　ｘｘ　　　　　　　　　　　　　　　　　　　　ｘｘｘ　　　ｘｘｘ　　ｘｘｘｘｘｘｘｘｘ　　　　　　　　　　　　　　　　　
　　　　　　　　　　　　ｘｘｘｘｘ　　　　ｘｘｘ　ｘ　　　　ｘｘｘ　　　　　　　　　　　　　　　　　　　　ｘｘｘ　　　ｘｘｘ　　　　ｘｘｘｘｘｘｘ　　　　　　　　　　　　　　　　　
　　　　　　　　　　　　ｘｘｘ　　　　　ｘｘｘ　　ｘｘ　　　ｘｘｘ　　　　　　　　　　　　　　　　　　　　ｘｘｘ　　ｘｘｘ　　　　　　　ｘｘｘｘｘｘ　　　　　　　　　　　　　　　　
　　　　　　　　　　　　ｘｘｘ　　　　ｘｘｘ　　　ｘｘｘｘ　ｘｘ　　　　　　　　　　　　　　　　　　　　　ｘｘｘ　ｘｘｘ　　　　　　　　　　ｘｘｘｘｘｘ　　　　　　　　　　　　　　
　　　　　　　　　　　　ｘｘｘ　　　ｘｘｘ　　　　　ｘｘｘｘｘｘ　　　　　　　　　　　　　　　　　　　　　ｘｘｘ　ｘｘｘ　　　　　　　　　　　　ｘｘｘｘｘｘ　　　　　　　　　　　　
　　　　　　　　　　　　ｘｘｘ　ｘｘｘ　　　　　　　　ｘｘｘｘｘ　　　　　　　　　　　　　　　　　　　　　ｘｘｘｘｘｘ　　　　　　　　　　　　　　　ｘｘｘｘｘｘｘｘｘｘ　　　　　　
　　　　　　　　　　　　　ｘｘ　　ｘ　　　　　　　　　　ｘｘｘ　　　　　　　　　　　　　　　　　　　　　　ｘｘｘｘｘ　　　　　　　　　　　　　　　　　　ｘｘｘｘｘｘ　　　　　　　　
　　　　　　　　　　　　　ｘｘ　　　　　　　　　　　　　　ｘｘ　　　　　　　　　　　　　　　　　　　　　　　ｘｘｘ　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　";
            var arr = temp.Split(new char[]{'\n','\r'},StringSplitOptions.RemoveEmptyEntries);
            var result = string.Join("',\n\r'", arr);
            var final = "'" + result + "'";
        }

        [TestMethod]
        public void TestStr()
        {
            var height = 100;
            var width = 100;

            //产生字模
            //Bitmap bmp = new Bitmap(height, width);
            //Graphics g = Graphics.FromImage(bmp);
            //g.FillRectangle(Brushes.White, new Rectangle() { X = 0, Y = 0, Height = height, Width = width });
            //g.DrawString("A", new Font(FontFamily.GenericSerif,10), Brushes.Black, new PointF() { X = Convert.ToSingle(1), Y = Convert.ToSingle(1) });
            

            var bmp = TextToBitmap("杨", new Font("微软雅黑", 30), Rectangle.Empty, Color.Black,Color.AliceBlue);
            bmp.Save("c:\\save.jpg", ImageFormat.Jpeg);
            //var result = string.Join("", Enumerable.Range(0, 256).Select(a => new { x = a % bmp.Height, y = a / bmp.Width })
            //    .Select(x => bmp.GetPixel(x.x, x.y).GetBrightness() > 0.5f ? " " : "1"));

            var final = new List<string>();
            for (int hIndex = 0; hIndex < bmp.Height; hIndex++)
            {
                var line = "";
                for (int wIndex = 0; wIndex < bmp.Width; wIndex++)
                {
                 
                    var temp = bmp.GetPixel(wIndex, hIndex).GetBrightness() > 0.5f ? " " : "0";
                    line += temp;
                    
                }
                final.Add($"'{line}',\n\r");
            }
            var result = string.Join(' ', final);

        }

        private Bitmap TextToBitmap(string text, Font font, Rectangle rect, Color fontcolor, Color backColor)
        {
            Graphics g;
            Bitmap bmp;
            StringFormat format = new StringFormat(StringFormatFlags.NoClip);
            if (rect == Rectangle.Empty)
            {
                bmp = new Bitmap(1, 1);
                g = Graphics.FromImage(bmp);
                //计算绘制文字所需的区域大小（根据宽度计算长度），重新创建矩形区域绘图
                SizeF sizef = g.MeasureString(text, font, PointF.Empty, format);

                int width = (int)(sizef.Width + 1);
                int height = (int)(sizef.Height + 1);
                rect = new Rectangle(0, 0, width, height);
                bmp.Dispose();

                bmp = new Bitmap(width, height);
            }
            else
            {
                bmp = new Bitmap(rect.Width, rect.Height);
            }

            g = Graphics.FromImage(bmp);

            //使用ClearType字体功能
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            g.FillRectangle(new SolidBrush(backColor), rect);
            g.DrawString(text, font, Brushes.Black, rect, format);
            return bmp;
        }
    }
}
