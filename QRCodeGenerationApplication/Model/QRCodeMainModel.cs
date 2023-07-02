using QRCoder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace QRCodeGenerationApplication.Model
{
    public abstract class QRCodeMainModel : INotifyPropertyChanged
    {
        #region PrivateFields

        private Bitmap? _qrCodeImage = null;
        private Bitmap? _qrCodeIcon = null;
        private QRCodeGenerator.ECCLevel _eccLevel = QRCodeGenerator.ECCLevel.H;
        private Color _lightColor = Color.FromArgb(255, 255, 255, 255);
        private Color _darkColor = Color.FromArgb(255, 0, 0, 0);
        private Color _iconBackgroundColor = Color.FromArgb(255, 255, 255, 255);

        #endregion

        #region PublicFields

        public QRCodeGenerator.ECCLevel ECCLevel
        {
            get => _eccLevel;
            set
            {

                _eccLevel = value;
                OnPropertyChanged();
            }
        }
        public Array GetECCLevels
        {
            get
            {
                return Enum.GetValues(typeof(QRCodeGenerator.ECCLevel));
            }
        }
        public Color LightColor
        {
            get => _lightColor;
            set
            {
                _lightColor = value;
                OnPropertyChanged();
            }
        }
        public Color DarkColor
        {
            get => _darkColor;
            set
            {
                _darkColor = value;
                OnPropertyChanged();
            }
        }
        public Color IconBackgroundColor
        {
            get => _iconBackgroundColor;
            set
            {
                _iconBackgroundColor = value;
                OnPropertyChanged();
            }
        }
        public Bitmap? QRCodeImage
        {
            get
            {
                if (_qrCodeImage == null)
                    return null;

                return _qrCodeImage;
            }
            set
            {
                if (_qrCodeImage != null)
                    GC.SuppressFinalize(_qrCodeImage);

                _qrCodeImage = value;
                OnPropertyChanged();
            }
        }
        public Bitmap? QRCodeIcon
        {
            get => _qrCodeIcon;
            set
            {
                if (value == null)
                    return;
                if (_qrCodeIcon != null)
                    _qrCodeIcon.Dispose();
                _qrCodeIcon = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(this.IsQRCodeIconNull));
            }
        }
        public bool IsQRCodeIconNull => this.QRCodeIcon == null ? true : false;
        public virtual string Payload { get; } = "";

        #endregion

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
