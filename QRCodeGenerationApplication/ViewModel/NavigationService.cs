using QRCodeGenerationApplication.View;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace QRCodeGenerationApplication.ViewModel
{
    class NavigationService : INotifyPropertyChanged
    {
        private object _contentOfPage = new object();

        private GenerateFromStringToQr? _generateFromStringToQr;
        private BookmarkQrCodePage? _bookmarkQrCodePage;
        private CalendarQrCodePage? _calendarQrCodePage;

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
                        this.Content = _generateFromStringToQr ?? (
                            _generateFromStringToQr = new GenerateFromStringToQr()
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
                            _bookmarkQrCodePage = new BookmarkQrCodePage()
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
                        this.Content = _calendarQrCodePage ?? (
                            _calendarQrCodePage = new CalendarQrCodePage()
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
