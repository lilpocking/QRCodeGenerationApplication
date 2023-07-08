using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace QRCodeGenerationApplication.ViewModel
{
    class NavigationService : INotifyPropertyChanged
    {
        private object _contentOfPage = new object();

        private Command? _navigateToCreateQRCodePage;
        private Command? _navigateToCreateBookmarkQRCodePage;
        private Command? _navigateToCreateCalendarQRCodePage;

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
                        this.Content = _contentOfPage ?? (
                        _contentOfPage = new ViewModel.GenerateFromStringToQr()
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
                        this.Content = _contentOfPage ?? (
                        _contentOfPage = new ViewModel.BookmarkQrCodePage()
                        );
                    }
            ));
        }
        public Command NavigateToCreateCalendarQRCodePage
        {
            get => _navigateToCreateCalendarQRCodePage ?? (
                _navigateToCreateCalendarQRCodePage = new Command(
                    obj =>
                    {
                        this.Content = _contentOfPage ?? (
                        _contentOfPage = new ViewModel.BookmarkQrCodePage()
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
