using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spire.Pdf;
using Spire.Pdf.Graphics;
using System.Drawing;
using System.IO;

namespace Common
{
    public class SpirePdfHelper
    {
        /// <summary>
        /// 将PDF文件第一页转成图片
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static Image LoadPdfFirstPage(string fileName)
        {
            Image image = null;
            
            //open pdf document
            PdfDocument doc = null;

            try
            {
                doc = new PdfDocument();
                doc.LoadFromFile(fileName);
                if (doc.Pages.Count > 0)
                {
                    image = doc.SaveAsImage(0);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (doc != null)
                {
                    doc.Close();
                }
            }
            return image;
        }
        /// <summary>
        /// 将图片转成PDF
        /// </summary>
        /// <param name="fromFileName"></param>
        /// <param name="toFileName"></param>
        /// <returns></returns>
        public static bool SaveToPdf(string imageName, string toFileName)
        {
            bool result = false;

            FileStream fs = null;
            try
            {
                fs = new FileStream(imageName, FileMode.Open, FileAccess.Read);
                Spire.Pdf.Graphics.PdfImage image = Spire.Pdf.Graphics.PdfImage.FromStream(fs);
                result = SaveFile(image, toFileName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }
            }
            return result;
        }
        /// <summary>
        /// 将字节转成PDF
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="toFileName"></param>
        /// <returns></returns>
        public static bool SaveToPdf(byte[] imageBuffer, string toFileName)
        {
            bool result = false;

            MemoryStream ms = null;
            try
            {
                ms = new MemoryStream(imageBuffer);
                Spire.Pdf.Graphics.PdfImage image = Spire.Pdf.Graphics.PdfImage.FromStream(ms);
                result = SaveFile(image, toFileName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (ms != null)
                {
                    ms.Close();
                }
            }
            return result;
        }
        /// <summary>
        /// 将Image对像转成PDF
        /// </summary>
        /// <param name="fromImage"></param>
        /// <param name="toFileName"></param>
        /// <returns></returns>
        public static bool SaveToPdf(Image Image, string toFileName)
        {
            bool result = false;
            try
            {
                //fs = new FileStream(fromFileName, FileMode.Open, FileAccess.Read);
                //MemoryStream ms = new MemoryStream(buffer);
                Spire.Pdf.Graphics.PdfImage image = Spire.Pdf.Graphics.PdfImage.FromImage(Image);
                result = SaveFile(image, toFileName);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
        /// <summary>
        /// 将stream对像转成PDF
        /// </summary>
        /// <param name="Image"></param>
        /// <param name="toFileName"></param>
        /// <returns></returns>
        public static bool SaveToPdf(Stream Image, string toFileName)
        {
            bool result = false;

            try
            {
                //MemoryStream ms = new MemoryStream(buffer);
                Spire.Pdf.Graphics.PdfImage image = Spire.Pdf.Graphics.PdfImage.FromStream(Image);
                result = SaveFile(image, toFileName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="image"></param>
        /// <param name="toFileName"></param>
        /// <returns></returns>
        private static bool SaveFile(PdfImage image, string toFileName)
        {
            bool result = false;
            Spire.Pdf.PdfDocument doc = null;
            try
            {
                //Create a pdf document.
                doc = new Spire.Pdf.PdfDocument();

                // Create one page
                //PdfPageBase page = doc.Pages.Add(new SizeF(image.Width + 100, image.Height + 100));
                PdfPageBase page = doc.Pages.Add();
                
                //Draw the image
                page.Canvas.DrawImage(image, new PointF(0, 0), new SizeF(page.Canvas.Size));
                

                //Save pdf file.
                doc.SaveToFile(toFileName);

                result = true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (doc != null)
                {
                    doc.Close();
                }
            }
            return result;
        }
    }
}
