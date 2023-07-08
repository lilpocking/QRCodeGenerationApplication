using QRCodeGenerationApplication.Model;
using QRCodeGenerationApplication.ViewModel;
using System.Windows.Controls;

namespace QRCodeGenerationApplication.View
{
    /// <summary>
    /// Логика взаимодействия для GenerateFromStringToQr.xaml
    /// </summary>
    public partial class GenerateFromStringToQr : Page
    {
        public GenerateFromStringToQr()
        {
            InitializeComponent();
            QRCodeViewModel qRCodeViewModel = new QRCodeViewModel();
            qRCodeViewModel.QRCode = new QRCodeModel();
            this.DataContext = qRCodeViewModel;
        }
    }
}
