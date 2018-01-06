using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseHelp_Core.Helper
{
    public class ToImgHelper
    {
        /// <summary>
        /// 把文字转成图片并输出Base64字符串
        /// </summary>
        /// <param name="validateCode"></param>
        /// <returns></returns>
        public static string CreateGraphic(string content)
        {
            //创建位图对象
            Bitmap objBitmap = null;
            //创建绘图图面对象
            Graphics g = null;
            //创建并初始化字体对象
            Font stringFont = new Font("宋体", 10, FontStyle.Regular);
            //文本布局对象
            StringFormat stringFormat = new StringFormat();
            //设置文本格式
            stringFormat.FormatFlags = StringFormatFlags.NoWrap;
            //绘图过程
            try
            {
                //实例化位图对象
                objBitmap = new Bitmap(1, 1);
                //实例化绘图图面对象，将位图对象放入图面中
                g = Graphics.FromImage(objBitmap);
                g.Clear(Color.Transparent);

                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = SmoothingMode.None;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
                //获取欲绘制文字图片的高宽
                SizeF stringSize = g.MeasureString(content, stringFont);
                int nWidth = (int)stringSize.Width;
                int nHeight = (int)stringSize.Height;
                //获取高宽后释放无用的图面对象资源和位图对象资源
                g.Dispose();
                objBitmap.Dispose();
                //根据已获得的高宽实例化新的位图对象
                objBitmap = new Bitmap(nWidth, nHeight);
                //根据新的位图对象实例化新的绘图图面对象
                g = Graphics.FromImage(objBitmap);
                //填充图片，可以指定背景颜色，开始坐标和图片高宽，指定文本呈现样式
                g.FillRectangle(new SolidBrush(Color.White), new Rectangle(0, 0, nWidth, nHeight));
                //开始绘制
                g.DrawString(content, stringFont, new SolidBrush(Color.Black), new PointF(0, 0), stringFormat);
                //生成图片
                //保存图片数据
                MemoryStream stream = new MemoryStream();
                objBitmap.MakeTransparent(Color.Transparent);
                objBitmap.Save(stream, ImageFormat.Png);
                return Convert.ToBase64String(stream.ToArray());
            }
            catch (Exception ee)
            {
                return "";
            }
            finally
            {
                if (null != g) g.Dispose();
                if (null != objBitmap) objBitmap.Dispose();
            } 
        }
    }
}
