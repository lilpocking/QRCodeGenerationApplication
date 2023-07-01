using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Interop;
using System.Windows;
using System.Windows.Media.Imaging;

namespace QRCodeGenerationApplication.Internal
{
    internal class ImageConverter
    {
        private ImageConverter() { }

        #region BitmapImageToBitmap

        public static Bitmap BitmapImageToBitmap(BitmapImage image)
        {
            using (MemoryStream outStream = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(image));
                enc.Save(outStream);
                System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(outStream);

                return new Bitmap(bitmap);
            }
        }

        #endregion

        #region BitmapToBitmapImage

        public static BitmapImage BitmapToBitmapImageConvert(Bitmap bitmap, int DecodePixelHeight = 0, int DecodePixelWidth = 0)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Png);
                memory.Position = 0;
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.DecodePixelHeight = DecodePixelHeight;
                image.DecodePixelWidth = DecodePixelWidth;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = memory;
                image.EndInit();
                image.Freeze();
                return image;
            }
        }
        public static BitmapImage BitmapToBitmapImageConvert(in Bitmap bitmap, int DecodePixelHeight = 0, int DecodePixelWidth = 0)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Png);
                memory.Position = 0;
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.DecodePixelHeight = DecodePixelHeight;
                image.DecodePixelWidth = DecodePixelWidth;
                image.StreamSource = memory;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.EndInit();
                image.Freeze();
                return image;
            }
        }
        public static void BitmapToBitmapImageConvert(in Bitmap bitmap, out BitmapImage image, int DecodePixelHeight = 0, int DecodePixelWidth = 0)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Png);
                memory.Position = 0;
                image = new BitmapImage();
                image.BeginInit();
                image.DecodePixelHeight = DecodePixelHeight;
                image.DecodePixelWidth = DecodePixelWidth;
                image.StreamSource = memory;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.EndInit();
                image.Freeze();
            }
        }
        public static async Task<BitmapImage> BitmapToBitmapImageConvertAsync(Bitmap bitmap, int DecodePixelHeight = 0, int DecodePixelWidth = 0)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Png);
                memory.Position = 0;
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.DecodePixelHeight = DecodePixelHeight;
                image.DecodePixelWidth = DecodePixelWidth;
                await memory.CopyToAsync(image.StreamSource);
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.EndInit();
                image.Freeze();
                return image;
            }
        }

        #endregion
    }
}
