using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace QRCodeGenerationApplication.Internal
{
    internal class ImageConverter
    {
        private ImageConverter() { }

        #region BitmapImageToBitmap

        public static Bitmap BitmapImageToBitmap(BitmapImage image)
        {
            using(MemoryStream outStream = new MemoryStream())
            {
                image.StreamSource.CopyTo(outStream);
                return new Bitmap(outStream);
            }
        }

        #endregion

        #region BitmapToBitmapImage

        public static BitmapImage BitmapToBitmapImageConvert(Bitmap bitmap, int DecodePixelHeight = 0, int DecodePixelWidth = 0)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.DecodePixelHeight = DecodePixelHeight;
                image.DecodePixelWidth = DecodePixelWidth;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = memory;
                image.EndInit();
                return image;
            }
        }
        public static BitmapImage BitmapToBitmapImageConvert(in Bitmap bitmap, int DecodePixelHeight = 0, int DecodePixelWidth = 0)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.DecodePixelHeight = DecodePixelHeight;
                image.DecodePixelWidth = DecodePixelWidth;
                image.StreamSource = memory;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.EndInit();
                return image;
            }
        }
        public static void BitmapToBitmapImageConvert(in Bitmap bitmap, out BitmapImage image, int DecodePixelHeight = 0, int DecodePixelWidth = 0)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                image = new BitmapImage();
                image.BeginInit();
                image.DecodePixelHeight = DecodePixelHeight;
                image.DecodePixelWidth = DecodePixelWidth;
                image.StreamSource = memory;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.EndInit();
            }
        }
        public static async Task<BitmapImage> BitmapToBitmapImageConvertAsync(Bitmap bitmap, int DecodePixelHeight = 0, int DecodePixelWidth = 0)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.DecodePixelHeight = DecodePixelHeight;
                image.DecodePixelWidth = DecodePixelWidth;
                await memory.CopyToAsync(image.StreamSource);
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.EndInit();
                return image;
            }
        }

        #endregion
    }
}
