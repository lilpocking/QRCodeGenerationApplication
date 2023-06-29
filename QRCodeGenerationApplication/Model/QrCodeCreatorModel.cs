using Microsoft.Win32;
using QRCodeGenerationApplication.View;
using QRCoder;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace QRCodeGenerationApplication.Model
{
    public class QrCodeCreatorModel : INotifyPropertyChanged
    {
        private string _textToConvert = "";
        private BitmapImage? _qrCode = null;
        private BitmapImage? _qrCodeIcon = null;
        private QRCodeGenerator.ECCLevel _eccLevel = QRCodeGenerator.ECCLevel.H;
        private System.Windows.Media.Color _lightColorBrush = new System.Windows.Media.Color() { R =255, G =255, B=255 ,A=255};
        private System.Windows.Media.Color _darkColorBrush = new System.Windows.Media.Color() { R = 0, G = 0, B = 0, A = 255 };

        private Command? _createQrCode;
        private Command? _saveQrCode;
        private Command? _addIcon;

        public string TextToConvert
        {
            get => _textToConvert;
            set
            {
                _textToConvert = value;
                OnPropertyChanged();
            }
        }
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
        public System.Windows.Media.Color LightColorBrush
        {
            get => _lightColorBrush;
            set
            {
                _lightColorBrush = value;
                OnPropertyChanged();
            }
        }
        public System.Windows.Media.Color DarkColorBrush
        {
            get => _darkColorBrush;
            set
            {
                _darkColorBrush = value;
                OnPropertyChanged();
            }
        }

        public BitmapImage? QRCodeImage
        {
            get
            {
                if (_qrCode == null)
                {
                    return null;
                }

                return _qrCode;
            }
            set
            {
                if(_qrCode != null)
                {
                    GC.SuppressFinalize(_qrCode);
                }
                _qrCode = value;
                OnPropertyChanged();
            }
        }
        public BitmapImage? QRCodeIcon
        {
            get
            {
                if (_qrCodeIcon == null)
                {
                    return null;
                }
                return _qrCodeIcon;
            }
            set
            {
                _qrCodeIcon = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(this.IsQRCodeIconNull));
            }
        }

        public Command SaveQrCode
        {
            get
            {
                return _saveQrCode ?? (
                    _saveQrCode = new Command(
                        obj =>
                        {
                            SaveFileDialog saveFileDialog = new SaveFileDialog()
                            {
                                Filter =
                                "PNG image(*.PNG)|*.png" +
                                "|" +
                                "JPEG image(*.JPEG)|*.jpeg",
                            };
                            if (saveFileDialog.ShowDialog() == true)
                            {
                                JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                                encoder.Frames.Add(BitmapFrame.Create(this.QRCodeImage));
                                using (FileStream fileStream = new FileStream(saveFileDialog.FileName, FileMode.Create))
                                    encoder.Save(fileStream);
                            }
                        },
                        obj => { return _qrCode != null; }
                        )
                    );
            }
        }
        public Command AddIcon
        {
            get
            {
                return _addIcon ?? (
                    _addIcon = new Command(
                        obj =>
                        {
                            OpenFileDialog openFileDialog = new OpenFileDialog()
                            {
                                Filter =
                                "PNG image(*.PNG)|*.png" +
                                "|" +
                                "JPEG image(*.JPEG)|*.jpeg" +
                                "|" +
                                "ICO image(*.ICO)|*.ico",
                            };
                            if (openFileDialog.ShowDialog() == true)
                            {
                                QRCodeIcon = new BitmapImage(new Uri(openFileDialog.FileName));
                            }
                        }
                        )
                    );
            }
        }
        public Command CreateQrCode
        {
            get
            {
                return _createQrCode ?? (_createQrCode = new Command(
            obj =>
            {
                QRCoder.QRCodeGenerator qrGenerator = new QRCoder.QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(_textToConvert, this.ECCLevel);
                QRCode qrCode = new QRCode(qrCodeData);

                System.Drawing.Color LightColor = System.Drawing.Color.FromArgb(
                    this.LightColorBrush.R,
                    this.LightColorBrush.G, 
                    this.LightColorBrush.B);

                System.Drawing.Color DarkColor = System.Drawing.Color.FromArgb(
                    this.DarkColorBrush.R,
                    this.DarkColorBrush.G, 
                    this.DarkColorBrush.B);

                Bitmap qrCodeImage;

                if (this.QRCodeIcon != null && this.QRCodeIcon.UriSource != null)
                {
                    qrCodeImage = qrCode.GetGraphic(20, DarkColor, LightColor, icon: new Bitmap(this.QRCodeIcon.UriSource.LocalPath), iconBackgroundColor: System.Drawing.Color.White, iconBorderWidth: 1);
                }
                else
                {
                    qrCodeImage = qrCode.GetGraphic(20, DarkColor, LightColor, drawQuietZones: true);
                }

                this.QRCodeImage = Internal.ImageConverter.BitmapToBitmapImageConvert(in qrCodeImage);

                qrCodeImage.Dispose();
            },
            obj => { return !string.IsNullOrEmpty(_textToConvert); }
            ));
            }
        }
        
        public bool IsQRCodeIconNull => this.QRCodeIcon == null ? true : false;

        public void IconDrop(object sender, System.Windows.DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                if (files.Length == 1)
                {
                    this.QRCodeIcon = new BitmapImage(new Uri(files[0]));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
