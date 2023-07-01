using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Windows.Media.Imaging;
using QRCodeGenerationApplication.Model;
using QRCoder;
using System.Windows;

namespace QRCodeGenerationApplication.ViewModel
{
    public class QRCodeViewModel : INotifyPropertyChanged
    {
        private QRCodeModel _qrCode = new QRCodeModel();

        private Command? _createQrCode;
        private Command? _saveQrCode;
        private Command? _addIcon;

        public QRCodeModel QRCode
        {
            get => _qrCode;
            set
            {
                _qrCode = value;
                OnPropertyChanged();
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
                            if (this.QRCode.QRCodeImage == null)
                                return;

                            SaveFileDialog saveFileDialog = new SaveFileDialog()
                            {
                                Filter =
                                "PNG image(*.PNG)|*.png" +
                                "|" +
                                "JPEG image(*.JPEG)|*.jpeg"+
                                "|" +
                                "TIFF image(*.TIFF)|*.tiff",
                            };
                            if (saveFileDialog.ShowDialog() == true)
                            {
                                /*JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                                encoder.Frames.Add(BitmapFrame.Create(this.QRCodeImage));*/

                                using (FileStream fileStream = new FileStream(saveFileDialog.FileName, FileMode.Create))
                                {
                                    switch (Path.GetExtension(saveFileDialog.FileName))
                                    {
                                        case ".png":
                                            this.QRCode.QRCodeImage.Save(fileStream, System.Drawing.Imaging.ImageFormat.Png);
                                            break;
                                        case ".jpeg":
                                            this.QRCode.QRCodeImage.Save(fileStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                                            break;
                                        case ".tiff":
                                            this.QRCode.QRCodeImage.Save(fileStream, System.Drawing.Imaging.ImageFormat.Tiff);
                                            break;
                                    }
                                }
                            }
                        },
                        obj => { return this.QRCode.QRCodeImage != null; }
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
                                this.QRCode.QRCodeIcon = new Bitmap(openFileDialog.FileName);
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
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(
                    this.QRCode.TextToConvert, 
                    this.QRCode.ECCLevel);
                QRCode qrCode = new QRCode(qrCodeData);

                if (this.QRCode.QRCodeIcon != null)
                {
                    this.QRCode.QRCodeImage = qrCode.GetGraphic(
                        20, 
                        this.QRCode.DarkColor, 
                        this.QRCode.LightColor, 
                        icon: this.QRCode.QRCodeIcon, 
                        iconBackgroundColor: QRCode.IconBackgroundColor, 
                        iconBorderWidth: 1);
                }
                else
                {
                    this.QRCode.QRCodeImage = qrCode.GetGraphic(
                        20, 
                        this.QRCode.DarkColor, 
                        this.QRCode.LightColor, 
                        drawQuietZones: true);
                }
            },
            obj => { return !string.IsNullOrEmpty(this.QRCode.TextToConvert); }
            ));
            }
        }

        public void IconDrop(object sender, System.Windows.DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                if (files.Length == 1)
                    this.QRCode.QRCodeIcon = new Bitmap(files[0]);
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
