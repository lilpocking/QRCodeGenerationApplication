using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRCodeGenerationApplication.Internal
{
    internal class ColorConverter
    {
        private ColorConverter() { }

        public static System.Windows.Media.Color DrawingColorToMediaColor(System.Drawing.Color color)
            => new System.Windows.Media.Color() { R=color.R, G=color.G, B=color.B, A=color.A};

        public static System.Drawing.Color MediaColorToDrawingColor(System.Windows.Media.Color color)
            => System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B);
    }
}
