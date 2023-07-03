using QRCodeGenerationApplication.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace QRCodeGenerationApplication.ViewModel
{
    class NavigationService : INotifyPropertyChanged
    {
        private object _contentOfPage = new object();
        private ViewModel.GenerateFromStringToQr? _createQrCodePage;
        private ViewModel.BookmarkQrCodePage? _bookmarkQrCodePage;
        private ViewModel.test? _createTestPage;

        private Command? _navigateToCreateQRCodePage;
        private Command? _navigateToCreateBookmarkQRCodePage;
        private Command? _navigateToTestPage;

        public object Content
        {
            get => _contentOfPage;
            set
            {
                _contentOfPage = value;
                OnPropertyChanged();
            }
        }

        public Command NavigateToCreateQRCodePage
        {
            get => _navigateToCreateQRCodePage ?? (
                _navigateToCreateQRCodePage = new Command(
                    obj =>
                    {
                        this.Content = _createQrCodePage ?? (
                        _createQrCodePage = new ViewModel.GenerateFromStringToQr()
                        );
                    }
            ));
        }
        public Command NavigateToCreateBookmarkQRCodePage
        {
            get => _navigateToCreateBookmarkQRCodePage ?? (
                _navigateToCreateBookmarkQRCodePage = new Command(
                    obj =>
                    {
                        this.Content = _bookmarkQrCodePage ?? (
                        _bookmarkQrCodePage = new ViewModel.BookmarkQrCodePage()
                        );
                    }
            ));
        }
        public Command NavigateToTestPage
        {
            get => _navigateToTestPage ?? (
                _navigateToTestPage = new Command(
                    obj =>
                    {
                        this.Content = _createTestPage ?? (
                        _createTestPage = new ViewModel.test()
                        );
                    }
            ));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
