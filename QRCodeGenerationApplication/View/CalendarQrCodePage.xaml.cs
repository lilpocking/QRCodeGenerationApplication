using QRCodeGenerationApplication.Model;
using QRCodeGenerationApplication.ViewModel;
using System.Windows.Controls;

namespace QRCodeGenerationApplication.View
{
    /// <summary>
    /// Логика взаимодействия для CalendarQrCodePage.xaml
    /// </summary>
    public partial class CalendarQrCodePage : Page
    {
        public CalendarQrCodePage()
        {
            InitializeComponent();
            QRCodeViewModel qRCodeViewModel = new QRCodeViewModel();
            qRCodeViewModel.QRCode = new CalendarEventsQRCodeModel();
            this.DataContext = qRCodeViewModel;
        }
    }
}
