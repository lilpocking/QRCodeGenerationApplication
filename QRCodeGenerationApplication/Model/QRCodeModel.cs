using QRCoder;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Media.Imaging;

namespace QRCodeGenerationApplication.Model
{
    public class QRCodeModel : QRCodeMainModel
    {
        #region PrivateFields

        private string _textToConvert = "";

        #endregion

        #region PublicFields

        public string TextToConvert
        {
            get => _textToConvert;
            set
            {
                _textToConvert = value;
                OnPropertyChanged();
            }
        }

        #endregion
    }
}
