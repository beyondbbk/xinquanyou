using System;
using System.Collections.Generic;

using System.DrawingCore;
using System.DrawingCore.Imaging;
using System.Linq;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using MySoft.Common;

//using SixLabors.Fonts;


namespace MySoft.Weeding.Services
{
    public class CreateStrArray
    {
        public static List<string> GetFormBmp(string imgName,int width,int height)
        {
            var list = new List<string>();
            Image img = Image.FromFile(@"C:\1.jpg");
            Bitmap bmpobj = (Bitmap)img;
            Bitmap result = new Bitmap(width, height);
            var heightRatio = (double)bmpobj.Height / result.Height;
            var widthRatio = (double)bmpobj.Width / result.Width;
            for (int i = 0; i < result.Height; i++)
            {
                var temp = new List<string>();
                for (int j = 0; j < result.Width; j++)
                {
                    //int tmpValue = GetGrayNumColor(bmpobj.GetPixel(j, i));

                    //bmpobj.SetPixel(j, i, Color.FromArgb(tmpValue, tmpValue, tmpValue));
                    //color

                    var color = bmpobj.GetPixel(Convert.ToInt32(j * widthRatio), Convert.ToInt32(i * heightRatio));
                    temp.Add($"rgb({color.R},{color.G},{color.B})");
                    //var resultWidth = (int)Math.Floor(j / widthRatio);
                    //var resultHeight = (int)Math.Floor(i / heightRatio);

                    //Debug.WriteLine("Width:"+resultWidth + "Height:"+resultHeight);
                    result.SetPixel(j, i, color);
                }
                list.Add(string.Join('#', temp));
            }
            Image img1 = (Image)result;
            img1.Save(@"C:/hui.jpg");
            return list;
        }

        private static int  GetGrayNumColor(System.DrawingCore.Color posClr)
        {
            return (posClr.R * 19595 + posClr.G * 38469 + posClr.B * 7472) >> 16;
        }
        public static List<string> GetFormText(string str, int fontSize, string fontName)
        {
            //产生字模
            //Bitmap bmp = new Bitmap(height, width);
            //Graphics g = Graphics.FromImage(bmp);
            //g.FillRectangle(Brushes.White, new Rectangle() { X = 0, Y = 0, Height = height, Width = width });
            //g.DrawString("A", new Font(FontFamily.GenericSerif,10), Brushes.Black, new PointF() { X = Convert.ToSingle(1), Y = Convert.ToSingle(1) });

            var bmp = TextToBitmap(str, new Font(fontName, fontSize), Rectangle.Empty, Color.Black, Color.AliceBlue);
            //bmp.Save("c:\\save.jpg", ImageFormat.Jpeg);
            //var result = string.Join("", Enumerable.Range(0, 256).Select(a => new { x = a % bmp.Height, y = a / bmp.Width })
            //    .Select(x => bmp.GetPixel(x.x, x.y).GetBrightness() > 0.5f ? " " : "1"));

            var final = new List<string>();
            for (int hIndex = 0; hIndex < bmp.Height; hIndex++)
            {
                var line = "";
                for (int wIndex = 0; wIndex < bmp.Width; wIndex++)
                {

                    var temp = bmp.GetPixel(wIndex, hIndex).GetBrightness() > 0.5f ? "1" : "0";
                    line += temp;

                }
                if (line.Any(u => u != '1'))
                {
                    final.Add(line);
                }

            }

            var startIndex = 0;
            var endIndex = 0;

            var tempCpunt = new List<int>();
            for (int i = 0; i < final[0].Length; i++)
            {
                var count = final.Where(u => u[i] == '0').Count();
                tempCpunt.Add(count);
                if (count == 0)
                {
                    startIndex = i;
                }
                else
                {
                    break;
                }
            }
            for (int i = final[0].Length-1; i>0; i--)
            {
                var count = final.Where(u => u[i] == '0').Count();
                if (count == 0)
                {
                    endIndex = i;
                }
                else
                {
                    break;
                }
            }


            var newTemp = new List<string>();
            for (int i = 0; i < final.Count; i++)
            {
                LogHelper.Debug("strat:"+startIndex + " end:" +endIndex+" end-start:"+(endIndex-startIndex));
                newTemp.Add(final[i].Substring(startIndex, endIndex - startIndex));
            }

            return newTemp;
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
                //Size size = TextRenderer.RenderTextTo() (text, font);

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
            Thread.Sleep(1000);
            g.Dispose();
            return bmp;

        }

    }
}

