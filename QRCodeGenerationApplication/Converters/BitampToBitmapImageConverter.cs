using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace QRCodeGenerationApplication.Converters
{

    [ValueConversion(typeof(Bitmap), typeof(BitmapImage), ParameterType = typeof(int))]
    public class BitmapToBitmapImageConverter : IValueConverter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter">decode pixel height</param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;
            if (value.GetType() == typeof(Bitmap))
            {
                Bitmap bitmap = (Bitmap)value;
                BitmapImage? image = null;

                if (parameter != null)
                {
                    int decodeHeight;
                    if (int.TryParse(parameter.ToString(), out decodeHeight))
                    {
                        Internal.ImageConverter.BitmapToBitmapImageConvert(in bitmap, out image, decodeHeight);
                    }
                }
                else
                    Internal.ImageConverter.BitmapToBitmapImageConvert(in bitmap, out image);
                
                return image;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
