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
                if (string.IsNullOrEmpty(value))
                {
                    errors[nameof(this.TextToConvert)] = "Value can't be null or empty";
                }
                else
                {
                    errors[nameof(this.TextToConvert)] = "";
                }
                _textToConvert = value;

                OnPropertyChanged();
            }
        }

        #endregion

        #region OverridePublicFields

        public override string Payload
        {
            get
            {
                return TextToConvert;
            }
        }

        #endregion
    }
}
