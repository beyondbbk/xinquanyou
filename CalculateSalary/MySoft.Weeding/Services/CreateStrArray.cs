using System;
using System.Collections.Generic;

using System.DrawingCore;
using System.DrawingCore.Imaging;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace MySoft.Weeding.Services
{
    public class CreateStrArray
    {
        public static List<string> Get(string str,int fontSize,string fontName)
        {
            //产生字模
            //Bitmap bmp = new Bitmap(height, width);
            //Graphics g = Graphics.FromImage(bmp);
            //g.FillRectangle(Brushes.White, new Rectangle() { X = 0, Y = 0, Height = height, Width = width });
            //g.DrawString("A", new Font(FontFamily.GenericSerif,10), Brushes.Black, new PointF() { X = Convert.ToSingle(1), Y = Convert.ToSingle(1) });


            var bmp = TextToBitmap(str, new Font(fontName, fontSize), Rectangle.Empty, Color.Black, Color.AliceBlue);
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
            return final;
        }

        private static Bitmap TextToBitmap(string text, Font font, Rectangle rect, Color fontcolor, Color backColor)
        {
            Graphics g;
            Bitmap bmp;
            StringFormat format = new StringFormat(StringFormat.GenericTypographic);
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;
            if (rect == Rectangle.Empty)
            {
                //StringFormat format1 = new StringFormat();
                //format.Alignment = StringAlignment.Center;
                //format.LineAlignment = StringAlignment.Center;
                //Size size = TextRenderer.MeasureText(text, font);

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
      
            g.FillRectangle(new SolidBrush(backColor), rect);
            g.DrawString(text, font, Brushes.Black, rect, format);
            return bmp;
        }
    }
}
